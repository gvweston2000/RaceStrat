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
    public class VehiclesControllerTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<InMemoryDatabaseFixture>, IAsyncLifetime
    {
        private readonly HttpClient _client;
        private readonly InMemoryDatabaseFixture _fixture;
        private static readonly SemaphoreSlim _semaphore = new(1, 1);

        public VehiclesControllerTests(WebApplicationFactory<Program> factory, InMemoryDatabaseFixture fixture)
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
        public async Task GetVehicles_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/api/vehicles");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetVehicle_ReturnsSuccessStatusCode_WhenVehicleExists()
        {
            var response = await _client.GetAsync($"/api/vehicles/{_fixture.TestVehicle.Id}");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetVehicle_ReturnsNotFound_WhenVehicleDoesNotExist()
        {
            var response = await _client.GetAsync("/api/vehicles/9999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetVehicle_ReturnsBadRequest_WhenIdIsInvalid()
        {
            var response = await _client.GetAsync("/api/vehicles/abc");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion

        #region POST Requests

        [Fact]
        public async Task PostVehicle_ReturnsCreatedAtAction_WhenVehicleIsValid()
        {
            var vehicle = new Vehicle
            {
                Name = "Test Vehicle",
                Weight = 800,
                EngineSize = 2.0,
                FuelEfficiency = 5.0,
                FuelTankCapacity = 50.0
            };

            var response = await _client.PostAsJsonAsync("/api/vehicles", vehicle);
            response.EnsureSuccessStatusCode();

            var createdVehicle = await response.Content.ReadFromJsonAsync<Vehicle>();

            Assert.NotNull(createdVehicle);
            Assert.Equal(vehicle.Name, createdVehicle.Name);
        }

        [Fact]
        public async Task PostVehicle_ReturnsBadRequest_WhenInvalidDataIsProvided()
        {
            var vehicle = new Vehicle { EngineSize = 2.0, FuelEfficiency = 5.0 };

            var response = await _client.PostAsJsonAsync("/api/vehicles", vehicle);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostVehicle_ReturnsBadRequest_WhenNameIsMissing()
        {
            var vehicle = new Vehicle
            {
                Weight = 800,
                EngineSize = 2.0,
                FuelEfficiency = 5.0,
                FuelTankCapacity = 50.0
            };

            var response = await _client.PostAsJsonAsync("/api/vehicles", vehicle);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion

        #region PUT Requests

        [Fact]
        public async Task PutVehicle_ReturnsNoContent_WhenVehicleUpdated()
        {
            _fixture.TestVehicle.Name = "Updated Vehicle";

            var response = await _client.PutAsJsonAsync($"/api/vehicles/{_fixture.TestVehicle.Id}", _fixture.TestVehicle);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task PutVehicle_ReturnsNotFound_WhenVehicleDoesNotExist()
        {
            var vehicle = new Vehicle
            {
                Name = "Updated Vehicle",
                Weight = 900,
                EngineSize = 2.5,
                FuelEfficiency = 6.0,
                FuelTankCapacity = 55.0
            };

            var response = await _client.PutAsJsonAsync("/api/vehicles/9999", vehicle);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        #endregion

        #region DELETE Requests

        [Fact]
        public async Task DeleteVehicle_ReturnsNoContent_WhenVehicleDeleted()
        {
            var deleteResponse = await _client.DeleteAsync($"/api/vehicles/{_fixture.TestVehicle.Id}");
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }

        [Fact]
        public async Task DeleteVehicle_ReturnsNotFound_WhenVehicleDoesNotExist()
        {
            var response = await _client.DeleteAsync("/api/vehicles/9999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteVehicle_ReturnsBadRequest_WhenIdIsInvalid()
        {
            var response = await _client.DeleteAsync("/api/vehicles/abc");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion
    }
}
