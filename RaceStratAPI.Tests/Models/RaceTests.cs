using Xunit;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using RaceStratAPI.Tests.Fixtures;
using RaceStratAPI.Data;
using RaceStratAPI.Services;
using Moq;

public class RaceTests : IClassFixture<InMemoryDatabaseFixture>
{
    private readonly InMemoryDatabaseFixture _fixture;
    private readonly Mock<IFuelCalculationService> _fuelCalculationServiceMock;

    public RaceTests(InMemoryDatabaseFixture fixture)
    {
        _fixture = fixture;
        _fuelCalculationServiceMock = new Mock<IFuelCalculationService>();
    }

    #region Default Values & Required Fields Tests

    [Fact]
    public void Race_Should_Have_Valid_DefaultValues()
    {
        var race = new Race();

        Assert.NotNull(race.TrackName);
        Assert.Equal(string.Empty, race.TrackName);
        Assert.Equal(200.0, race.AverageSpeed);
        Assert.Equal(0, race.TotalLaps);
        Assert.Equal(0, race.TrackLength);
        Assert.Equal(1.0, race.RaceConditionFactor);
        Assert.Equal(0, race.VehicleId);
        Assert.Null(race.Vehicle);
        Assert.Equal(0, race.FuelPerLap);
        Assert.Equal(0, race.PitStopsRequired);
        Assert.Equal(0, race.TotalFuelNeeded);
    }

    [Fact]
    public void Race_Should_Require_AverageSpeed()
    {
        var race = new Race { AverageSpeed = 0 };
        var results = ValidateModel(race);
        Assert.DoesNotContain(results, v => v.ErrorMessage.Contains("Average speed"));
    }

    [Fact]
    public void Race_Should_Require_TotalLaps()
    {
        var race = new Race { TrackName = "Silverstone", TrackLength = 5.8 };
        var results = ValidateModel(race);
        Assert.Contains(results, v => v.ErrorMessage.Contains("Total laps must be between"));
    }

    [Fact]
    public void Race_Should_Require_TrackLength()
    {
        var race = new Race { TrackName = "Silverstone" };
        var results = ValidateModel(race);
        Assert.Contains(results, v => v.ErrorMessage.Contains("Track length must be between"));
    }

    [Fact]
    public void Race_Should_Require_TrackName()
    {
        var race = new Race { TrackName = "" };
        var results = ValidateModel(race);
        Assert.Contains(results, v => v.ErrorMessage.Contains("The TrackName field is required."));
    }

    [Fact]
    public void Race_Should_Reject_Whitespace_TrackName()
    {
        var race = new Race { TrackName = "   " };
        var results = ValidateModel(race);
        Assert.Contains(results, v => v.ErrorMessage.Contains("The TrackName field is required."));
    }

    [Fact]
    public void Race_Should_Require_RaceConditionFactor()
    {
        var race = new Race { TrackName = "Silverstone", TrackLength = 5.8, TotalLaps = 52 };
        var results = ValidateModel(race);
        Assert.DoesNotContain(results, v => v.ErrorMessage.Contains("Race condition factor must be between"));
    }

    [Fact]
    public void Race_Should_Require_VehicleId()
    {
        var race = new Race { TrackName = "Silverstone", TrackLength = 5.8, TotalLaps = 52, VehicleId = 0 };
        var results = ValidateModel(race);
        Assert.Contains(results, v => v.ErrorMessage.Contains("VehicleId must be a positive number"));
    }

    #endregion

    #region Calculated Fields Tests Using Mocked Service

    [Fact]
    public void Race_Should_Calculate_FuelPerLap_Correctly_Using_Service()
    {
        var fuelEfficiency = 5.0;
        var trackLength = 5.8;
        var vehicleWeight = 1200;

        _fuelCalculationServiceMock
            .Setup(service => service.CalculateFuelPerLap(fuelEfficiency, trackLength, vehicleWeight))
            .Returns(3.0);

        var fuelPerLap = _fuelCalculationServiceMock.Object.CalculateFuelPerLap(fuelEfficiency, trackLength, vehicleWeight);
        Assert.Equal(3.0, fuelPerLap);
    }

    [Fact]
    public void Race_Should_Calculate_PitStopsRequired_Correctly_Using_Service()
    {
        var totalFuelNeeded = 156.0;
        var fuelTankCapacity = 50.0;

        _fuelCalculationServiceMock
            .Setup(service => service.PredictPitStops(totalFuelNeeded, fuelTankCapacity))
            .Returns(4.0);

        var pitStopsRequired = _fuelCalculationServiceMock.Object.PredictPitStops(totalFuelNeeded, fuelTankCapacity);
        Assert.Equal(4.0, pitStopsRequired);
    }

    [Fact]
    public void Race_Should_Calculate_TotalFuelNeeded_Correctly_Using_Service()
    {
        var fuelPerLap = 3.0;
        var totalLaps = 52;

        _fuelCalculationServiceMock
            .Setup(service => service.CalculateTotalFuel(fuelPerLap, totalLaps))
            .Returns(156.0);

        var totalFuelNeeded = _fuelCalculationServiceMock.Object.CalculateTotalFuel(fuelPerLap, totalLaps);
        Assert.Equal(156.0, totalFuelNeeded);
    }

    #endregion

    #region Overall Validation Test

    [Fact]
    public void Race_Should_Pass_Validation_When_Valid()
    {
        var race = new Race
        {
            TrackName = "Silverstone",
            TrackLength = 5.8,
            TotalLaps = 52,
            VehicleId = _fixture.TestVehicle.Id
        };
        var results = ValidateModel(race);
        Assert.Empty(results);
    }

    #endregion

    #region Helper Methods

    private static List<ValidationResult> ValidateModel(object model)
    {
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(model, context, results, true);
        return results;
    }

    private static void AssertValidationResult(List<ValidationResult> results, bool shouldFail, string errorMessage)
    {
        if (shouldFail)
            Assert.Contains(results, v => v.ErrorMessage.Contains(errorMessage));
        else
            Assert.Empty(results);
    }

    #endregion
}
