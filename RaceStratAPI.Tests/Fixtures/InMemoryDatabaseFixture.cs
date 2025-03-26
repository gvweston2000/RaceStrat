using Microsoft.EntityFrameworkCore;
using RaceStratAPI.Data;
using System;

namespace RaceStratAPI.Tests.Fixtures
{
    public class InMemoryDatabaseFixture : IDisposable
    {
        public ApplicationDbContext Context { get; private set; }
        public Vehicle TestVehicle { get; private set; }
        public Race TestRace { get; private set; }
        public string DatabaseName { get; private set; }

        public InMemoryDatabaseFixture()
        {
            Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Test");
            
            DatabaseName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(DatabaseName)
                .Options;
            Context = new ApplicationDbContext(options);
            SeedDatabase();
        }

        public async Task SeedDatabase()
        {
            TestVehicle = new Vehicle
            {
                Name = "Test Vehicle",
                EngineSize = 2.0,
                FuelEfficiency = 5.0,
                FuelTankCapacity = 50.0,
                Weight = 800
            };

            Context.Vehicles.Add(TestVehicle);
            Context.SaveChanges();

            TestRace = new Race
            {
                AverageSpeed = 200.0,
                TotalLaps = 52,
                TrackLength = 5.8,
                TrackName = "Test Race",
                RaceConditionFactor = 1.0,
                VehicleId = TestVehicle.Id
            };

            Context.Races.Add(TestRace);
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
