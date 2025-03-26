using Microsoft.EntityFrameworkCore;

namespace RaceStratAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Race> Races { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle { Id = 1, Name = "Dallara IR-18 (Team Penske - Chevrolet)", EngineSize = 2.2, FuelEfficiency = 2.1, FuelTankCapacity = 70, Weight = 725 },
                new Vehicle { Id = 2, Name = "Dallara IR-18 (Chip Ganassi Racing - Honda)", EngineSize = 2.2, FuelEfficiency = 2.0, FuelTankCapacity = 70, Weight = 725 },
                new Vehicle { Id = 3, Name = "Dallara IR-18 (Andretti Autosport - Honda)", EngineSize = 2.2, FuelEfficiency = 2.0, FuelTankCapacity = 70, Weight = 725 },
                new Vehicle { Id = 4, Name = "Dallara IR-18 (Arrow McLaren - Chevrolet)", EngineSize = 2.2, FuelEfficiency = 2.1, FuelTankCapacity = 70, Weight = 725 },
                new Vehicle { Id = 5, Name = "Dallara IR-18 (Rahal Letterman Lanigan Racing - Honda)", EngineSize = 2.2, FuelEfficiency = 2.0, FuelTankCapacity = 70, Weight = 725 },
                new Vehicle { Id = 6, Name = "Dallara IR-18 (Juncos Hollinger Racing - Chevrolet)", EngineSize = 2.2, FuelEfficiency = 2.1, FuelTankCapacity = 70, Weight = 725 },
                new Vehicle { Id = 7, Name = "Dallara IR-18 (Ed Carpenter Racing - Chevrolet)", EngineSize = 2.2, FuelEfficiency = 2.1, FuelTankCapacity = 70, Weight = 725 },
                new Vehicle { Id = 8, Name = "Dallara IR-18 (Foyt Racing - Honda)", EngineSize = 2.2, FuelEfficiency = 2.0, FuelTankCapacity = 70, Weight = 725 },
                new Vehicle { Id = 9, Name = "Dallara IR-18 (Meyer Shank Racing - Honda)", EngineSize = 2.2, FuelEfficiency = 2.0, FuelTankCapacity = 70, Weight = 725 }
            );

            modelBuilder.Entity<Race>().HasData(
                new Race
                {
                    Id = 1,
                    AverageSpeed = 225.0,
                    TotalLaps = 200,
                    TrackLength = 2.5,
                    TrackName = "Indianapolis Motor Speedway",
                    RaceConditionFactor = 1.2,
                    VehicleId = 1
                },
                new Race
                {
                    Id = 2,
                    AverageSpeed = 225.0,
                    TotalLaps = 200,
                    TrackLength = 2.5,
                    TrackName = "Indianapolis Motor Speedway",
                    RaceConditionFactor = 1.2,
                    VehicleId = 2
                },
                new Race
                {
                    Id = 3,
                    AverageSpeed = 225.0,
                    TotalLaps = 200,
                    TrackLength = 2.5,
                    TrackName = "Indianapolis Motor Speedway",
                    RaceConditionFactor = 1.2,
                    VehicleId = 3
                },
                new Race
                {
                    Id = 4,
                    AverageSpeed = 225.0,
                    TotalLaps = 200,
                    TrackLength = 2.5,
                    TrackName = "Indianapolis Motor Speedway",
                    RaceConditionFactor = 1.2,
                    VehicleId = 4
                },
                new Race
                {
                    Id = 5,
                    AverageSpeed = 225.0,
                    TotalLaps = 200,
                    TrackLength = 2.5,
                    TrackName = "Indianapolis Motor Speedway",
                    RaceConditionFactor = 1.2,
                    VehicleId = 5
                },
                new Race
                {
                    Id = 6,
                    AverageSpeed = 225.0,
                    TotalLaps = 200,
                    TrackLength = 2.5,
                    TrackName = "Indianapolis Motor Speedway",
                    RaceConditionFactor = 1.2,
                    VehicleId = 6
                },
                new Race
                {
                    Id = 7,
                    AverageSpeed = 225.0,
                    TotalLaps = 200,
                    TrackLength = 2.5,
                    TrackName = "Indianapolis Motor Speedway",
                    RaceConditionFactor = 1.2,
                    VehicleId = 7
                },
                new Race
                {
                    Id = 8,
                    AverageSpeed = 225.0,
                    TotalLaps = 200,
                    TrackLength = 2.5,
                    TrackName = "Indianapolis Motor Speedway",
                    RaceConditionFactor = 1.2,
                    VehicleId = 8
                },
                new Race
                {
                    Id = 9,
                    AverageSpeed = 225.0,
                    TotalLaps = 200,
                    TrackLength = 2.5,
                    TrackName = "Indianapolis Motor Speedway",
                    RaceConditionFactor = 1.2,
                    VehicleId = 9
                }
            );

            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle { Id = 10, Name = "Mercedes W14 (Mercedes-AMG Petronas - Mercedes)", EngineSize = 1.6, FuelEfficiency = 1.7, FuelTankCapacity = 110, Weight = 798 },
                new Vehicle { Id = 11, Name = "RB19 (Red Bull Racing - Honda)", EngineSize = 1.6, FuelEfficiency = 1.6, FuelTankCapacity = 110, Weight = 798 },
                new Vehicle { Id = 12, Name = "SF-23 (Ferrari - Ferrari)", EngineSize = 1.6, FuelEfficiency = 1.7, FuelTankCapacity = 110, Weight = 798 },
                new Vehicle { Id = 13, Name = "MCL60 (McLaren - Mercedes)", EngineSize = 1.6, FuelEfficiency = 1.7, FuelTankCapacity = 110, Weight = 798 },
                new Vehicle { Id = 14, Name = "A523 (Alpine - Renault)", EngineSize = 1.6, FuelEfficiency = 1.8, FuelTankCapacity = 110, Weight = 798 },
                new Vehicle { Id = 15, Name = "AMR23 (Aston Martin - Mercedes)", EngineSize = 1.6, FuelEfficiency = 1.7, FuelTankCapacity = 110, Weight = 798 },
                new Vehicle { Id = 16, Name = "C43 (Alfa Romeo - Ferrari)", EngineSize = 1.6, FuelEfficiency = 1.7, FuelTankCapacity = 110, Weight = 798 },
                new Vehicle { Id = 17, Name = "VF-23 (Haas - Ferrari)", EngineSize = 1.6, FuelEfficiency = 1.7, FuelTankCapacity = 110, Weight = 798 },
                new Vehicle { Id = 18, Name = "AT04 (AlphaTauri - Honda)", EngineSize = 1.6, FuelEfficiency = 1.6, FuelTankCapacity = 110, Weight = 798 },
                new Vehicle { Id = 19, Name = "FW46 (Williams - Mercedes)", EngineSize = 1.6, FuelEfficiency = 1.7, FuelTankCapacity = 110, Weight = 798 }
            );

            modelBuilder.Entity<Race>().HasData(
                new Race
                {
                    Id = 10,
                    AverageSpeed = 250.0,
                    TotalLaps = 52,
                    TrackLength = 5.891,
                    TrackName = "Silverstone Circuit",
                    RaceConditionFactor = 1.0,
                    VehicleId = 10
                },
                new Race
                {
                    Id = 11,
                    AverageSpeed = 250.0,
                    TotalLaps = 52,
                    TrackLength = 5.891,
                    TrackName = "Silverstone Circuit",
                    RaceConditionFactor = 1.0,
                    VehicleId = 11
                },
                new Race
                {
                    Id = 12,
                    AverageSpeed = 250.0,
                    TotalLaps = 52,
                    TrackLength = 5.891,
                    TrackName = "Silverstone Circuit",
                    RaceConditionFactor = 1.0,
                    VehicleId = 12
                },
                new Race
                {
                    Id = 13,
                    AverageSpeed = 250.0,
                    TotalLaps = 52,
                    TrackLength = 5.891,
                    TrackName = "Silverstone Circuit",
                    RaceConditionFactor = 1.0,
                    VehicleId = 13
                },
                new Race
                {
                    Id = 14,
                    AverageSpeed = 250.0,
                    TotalLaps = 52,
                    TrackLength = 5.891,
                    TrackName = "Silverstone Circuit",
                    RaceConditionFactor = 1.0,
                    VehicleId = 14
                },
                new Race
                {
                    Id = 15,
                    AverageSpeed = 250.0,
                    TotalLaps = 52,
                    TrackLength = 5.891,
                    TrackName = "Silverstone Circuit",
                    RaceConditionFactor = 1.0,
                    VehicleId = 15
                },
                new Race
                {
                    Id = 16,
                    AverageSpeed = 250.0,
                    TotalLaps = 52,
                    TrackLength = 5.891,
                    TrackName = "Silverstone Circuit",
                    RaceConditionFactor = 1.0,
                    VehicleId = 16
                },
                new Race
                {
                    Id = 17,
                    AverageSpeed = 250.0,
                    TotalLaps = 52,
                    TrackLength = 5.891,
                    TrackName = "Silverstone Circuit",
                    RaceConditionFactor = 1.0,
                    VehicleId = 17
                },
                new Race
                {
                    Id = 18,
                    AverageSpeed = 250.0,
                    TotalLaps = 52,
                    TrackLength = 5.891,
                    TrackName = "Silverstone Circuit",
                    RaceConditionFactor = 1.0,
                    VehicleId = 18
                },
                new Race
                {
                    Id = 19,
                    AverageSpeed = 250.0,
                    TotalLaps = 52,
                    TrackLength = 5.891,
                    TrackName = "Silverstone Circuit",
                    RaceConditionFactor = 1.0,
                    VehicleId = 19
                }
            );

            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle { Id = 20, Name = "Chevrolet Camaro ZL1 (Hendrick Motorsports - Chevrolet)", EngineSize = 5.8, FuelEfficiency = 3.2, FuelTankCapacity = 70, Weight = 1500 },
                new Vehicle { Id = 21, Name = "Ford Mustang GT (Team Penske - Ford)", EngineSize = 5.8, FuelEfficiency = 3.1, FuelTankCapacity = 70, Weight = 1500 },
                new Vehicle { Id = 22, Name = "Toyota Camry (Joe Gibbs Racing - Toyota)", EngineSize = 5.8, FuelEfficiency = 3.0, FuelTankCapacity = 70, Weight = 1500 },
                new Vehicle { Id = 23, Name = "Chevrolet Camaro ZL1 (Richard Childress Racing - Chevrolet)", EngineSize = 5.8, FuelEfficiency = 3.2, FuelTankCapacity = 70, Weight = 1500 },
                new Vehicle { Id = 24, Name = "Ford Mustang GT (Roush Fenway Racing - Ford)", EngineSize = 5.8, FuelEfficiency = 3.1, FuelTankCapacity = 70, Weight = 1500 },
                new Vehicle { Id = 25, Name = "Chevrolet Camaro ZL1 (Stewart-Haas Racing - Chevrolet)", EngineSize = 5.8, FuelEfficiency = 3.2, FuelTankCapacity = 70, Weight = 1500 },
                new Vehicle { Id = 26, Name = "Toyota Camry (Richard Petty Motorsports - Toyota)", EngineSize = 5.8, FuelEfficiency = 3.0, FuelTankCapacity = 70, Weight = 1500 },
                new Vehicle { Id = 27, Name = "Ford Mustang GT (Wood Brothers Racing - Ford)", EngineSize = 5.8, FuelEfficiency = 3.1, FuelTankCapacity = 70, Weight = 1500 },
                new Vehicle { Id = 28, Name = "Chevrolet Camaro ZL1 (JTG Daugherty Racing - Chevrolet)", EngineSize = 5.8, FuelEfficiency = 3.2, FuelTankCapacity = 70, Weight = 1500 },
                new Vehicle { Id = 29, Name = "Toyota Camry (Michael Waltrip Racing - Toyota)", EngineSize = 5.8, FuelEfficiency = 3.0, FuelTankCapacity = 70, Weight = 1500 }
            );

            modelBuilder.Entity<Race>().HasData(
                new Race
                {
                    Id = 20,
                    AverageSpeed = 180.0,
                    TotalLaps = 250,
                    TrackLength = 2.5,
                    TrackName = "Daytona International Speedway",
                    RaceConditionFactor = 1.2,
                    VehicleId = 20
                },
                new Race
                {
                    Id = 21,
                    AverageSpeed = 180.0,
                    TotalLaps = 250,
                    TrackLength = 2.5,
                    TrackName = "Daytona International Speedway",
                    RaceConditionFactor = 1.2,
                    VehicleId = 21
                },
                new Race
                {
                    Id = 22,
                    AverageSpeed = 180.0,
                    TotalLaps = 250,
                    TrackLength = 2.5,
                    TrackName = "Daytona International Speedway",
                    RaceConditionFactor = 1.2,
                    VehicleId = 22
                },
                new Race
                {
                    Id = 23,
                    AverageSpeed = 180.0,
                    TotalLaps = 250,
                    TrackLength = 2.5,
                    TrackName = "Daytona International Speedway",
                    RaceConditionFactor = 1.2,
                    VehicleId = 23
                },
                new Race
                {
                    Id = 24,
                    AverageSpeed = 180.0,
                    TotalLaps = 250,
                    TrackLength = 2.5,
                    TrackName = "Daytona International Speedway",
                    RaceConditionFactor = 1.2,
                    VehicleId = 24
                },
                new Race
                {
                    Id = 25,
                    AverageSpeed = 180.0,
                    TotalLaps = 250,
                    TrackLength = 2.5,
                    TrackName = "Daytona International Speedway",
                    RaceConditionFactor = 1.2,
                    VehicleId = 25
                },
                new Race
                {
                    Id = 26,
                    AverageSpeed = 180.0,
                    TotalLaps = 250,
                    TrackLength = 2.5,
                    TrackName = "Daytona International Speedway",
                    RaceConditionFactor = 1.2,
                    VehicleId = 26
                },
                new Race
                {
                    Id = 27,
                    AverageSpeed = 180.0,
                    TotalLaps = 250,
                    TrackLength = 2.5,
                    TrackName = "Daytona International Speedway",
                    RaceConditionFactor = 1.2,
                    VehicleId = 27
                },
                new Race
                {
                    Id = 28,
                    AverageSpeed = 180.0,
                    TotalLaps = 250,
                    TrackLength = 2.5,
                    TrackName = "Daytona International Speedway",
                    RaceConditionFactor = 1.2,
                    VehicleId = 28
                },
                new Race
                {
                    Id = 29,
                    AverageSpeed = 180.0,
                    TotalLaps = 250,
                    TrackLength = 2.5,
                    TrackName = "Daytona International Speedway",
                    RaceConditionFactor = 1.2,
                    VehicleId = 29
                }
            );
        }
    }
}
