using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using System.Net;
using RaceStratAPI.Data;
using RaceStratAPI.Tests.Fixtures;
using System.Threading;

namespace RaceStratAPI.Tests
{
    public class RacesControllerTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<InMemoryDatabaseFixture>, IAsyncLifetime
    {
        private readonly HttpClient _client;
        private readonly InMemoryDatabaseFixture _fixture;
        private static readonly SemaphoreSlim _semaphore = new(1, 1);

        public RacesControllerTests(WebApplicationFactory<Program> factory, InMemoryDatabaseFixture fixture)
        {
            _fixture = fixture;

            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseInMemoryDatabase(_fixture.DatabaseName));
                });
            }).CreateClient();
        }

        public async Task InitializeAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                await _fixture.SeedDatabase();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public Task DisposeAsync() => Task.CompletedTask;

        #region GET Requests

        [Fact]
        public async Task GetRaces_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/api/races");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetRace_ReturnsSuccessStatusCode_WhenRaceExists()
        {
            var response = await _client.GetAsync($"/api/races/{_fixture.TestRace.Id}");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetRace_ReturnsNotFound_WhenRaceDoesNotExist()
        {
            var response = await _client.GetAsync("/api/races/9999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetRace_ReturnsBadRequest_WhenIdIsInvalid()
        {
            var response = await _client.GetAsync("/api/races/abc");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion

        #region POST Requests

        [Fact]
        public async Task PostRace_ReturnsCreatedAtAction_WhenRaceIsValid()
        {
            var race = new Race
            {
                AverageSpeed = 200.0,
                TotalLaps = 52,
                TrackLength = 5.8,
                TrackName = "Test Race",
                RaceConditionFactor = 1.0,
                VehicleId = _fixture.TestVehicle.Id
            };

            var response = await _client.PostAsJsonAsync("/api/races", race);
            response.EnsureSuccessStatusCode();

            var createdRace = await response.Content.ReadFromJsonAsync<Race>();

            Assert.NotNull(createdRace);
            Assert.Equal(race.VehicleId, createdRace.VehicleId);
        }

        [Fact]
        public async Task PostRace_ReturnsBadRequest_WhenInvalidDataIsProvided()
        {
            var race = new Race { TrackLength = -5000, TotalLaps = 50 };
            var response = await _client.PostAsJsonAsync("/api/races", race);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostRace_ReturnsBadRequest_WhenVehicleIdIsMissing()
        {
            var race = new Race { TrackLength = 5000, TotalLaps = 50 };
            var response = await _client.PostAsJsonAsync("/api/races", race);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostRace_CalculatesFuelValuesCorrectly()
        {
            var race = new Race
            {
                AverageSpeed = 200.0,
                TotalLaps = 52,
                TrackLength = 5.8,
                TrackName = "Test Race",
                RaceConditionFactor = 1.0,
                VehicleId = _fixture.TestVehicle.Id
            };

            var response = await _client.PostAsJsonAsync("/api/races", race);
            response.EnsureSuccessStatusCode();

            var createdRace = await response.Content.ReadFromJsonAsync<Race>();

            Assert.True(createdRace.FuelPerLap > 0);
            Assert.True(createdRace.TotalFuelNeeded > 0);
            Assert.True(createdRace.PitStopsRequired >= 0);
        }

        [Fact]
        public async Task PostRace_ReturnsBadRequest_WhenTrackLengthIsZero()
        {
            var race = new Race { VehicleId = _fixture.TestVehicle.Id, TrackLength = 0, TotalLaps = 50 };
            var response = await _client.PostAsJsonAsync("/api/races", race);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostRace_ReturnsBadRequest_WhenTotalLapsIsZero()
        {
            var race = new Race { VehicleId = _fixture.TestVehicle.Id, TrackLength = 5000, TotalLaps = 0 };
            var response = await _client.PostAsJsonAsync("/api/races", race);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion

        #region PUT Requests

        [Fact]
        public async Task PutRace_ReturnsNoContent_WhenRaceUpdated()
        {
            _fixture.TestRace.VehicleId = _fixture.TestVehicle.Id;

            var putResponse = await _client.PutAsJsonAsync($"/api/races/{_fixture.TestRace.Id}", _fixture.TestRace);
            Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);
        }

        [Fact]
        public async Task PutRace_ReturnsNotFound_WhenRaceDoesNotExist()
        {
            var race = new Race
            {
                AverageSpeed = 200.0,
                TotalLaps = 52,
                TrackLength = 5.8,
                TrackName = "Test Race",
                RaceConditionFactor = 1.0,
                VehicleId = _fixture.TestVehicle.Id
            };

            var response = await _client.PutAsJsonAsync("/api/races/9999", race);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        #endregion

        #region DELETE Requests

        [Fact]
        public async Task DeleteRace_ReturnsNoContent_WhenRaceDeleted()
        {
            var deleteResponse = await _client.DeleteAsync($"/api/races/{_fixture.TestRace.Id}");
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }

        [Fact]
        public async Task DeleteRace_ReturnsNotFound_WhenRaceDoesNotExist()
        {
            var response = await _client.DeleteAsync("/api/races/9999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteRace_ReturnsBadRequest_WhenIdIsInvalid()
        {
            var response = await _client.DeleteAsync("/api/races/abc");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion
    }
}


