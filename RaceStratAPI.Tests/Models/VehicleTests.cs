using Xunit;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RaceStratAPI.Tests.Fixtures;
using RaceStratAPI.Data;

public class VehicleTests : IClassFixture<InMemoryDatabaseFixture>
{
    private readonly InMemoryDatabaseFixture _fixture;

    public VehicleTests(InMemoryDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    #region Engine Size Validation Tests

    [Fact]
    public void Vehicle_Should_Require_EngineSize()
    {
        var vehicle = new Vehicle { Name = "Test Vehicle" };
        var results = ValidateModel(vehicle);
        Assert.Contains(results, v => v.ErrorMessage.Contains("Engine size must be between"));
    }

    [Theory]
    [InlineData(0.5, true)]
    [InlineData(1.0, false)]
    [InlineData(10.0, false)]
    [InlineData(10.5, true)]
    [InlineData(-2.0, true)]
    [InlineData(100.0, true)]
    public void Vehicle_Should_Validate_EngineSize(double engineSize, bool shouldFail)
    {
        var vehicle = new Vehicle { Name = "Test Vehicle", EngineSize = engineSize, FuelEfficiency = 5.0, FuelTankCapacity = 50, Weight = 500 };
        var results = ValidateModel(vehicle);
        AssertValidationResult(results, shouldFail, "Engine size must be between");
    }

    #endregion

    #region Fuel Efficiency Validation Tests

    [Fact]
    public void Vehicle_Should_Require_FuelEfficiency()
    {
        var vehicle = new Vehicle { Name = "Test Vehicle", EngineSize = 2.0 };
        var results = ValidateModel(vehicle);
        Assert.Contains(results, v => v.ErrorMessage.Contains("Fuel efficiency must be between"));
    }

    [Theory]
    [InlineData(0.05, true)]
    [InlineData(0.1, false)]
    [InlineData(100.0, false)]
    [InlineData(150.0, true)]
    [InlineData(-5.0, true)]
    [InlineData(0.0, true)]
    public void Vehicle_Should_Validate_FuelEfficiency(double fuelEfficiency, bool shouldFail)
    {
        var vehicle = new Vehicle { Name = "Test Vehicle", EngineSize = 2.0, FuelEfficiency = fuelEfficiency, FuelTankCapacity = 50, Weight = 500 };
        var results = ValidateModel(vehicle);
        AssertValidationResult(results, shouldFail, "Fuel efficiency must be between");
    }

    #endregion

    #region Fuel Tank Capacity Validation Tests

    [Fact]
    public void Vehicle_Should_Require_FuelTankCapacity()
    {
        var vehicle = new Vehicle { Name = "Test Vehicle", EngineSize = 2.0, FuelEfficiency = 5.0 };
        var results = ValidateModel(vehicle);
        Assert.Contains(results, v => v.ErrorMessage.Contains("Fuel tank capacity must be between"));
    }

    [Theory]
    [InlineData(0, true)]
    [InlineData(1, false)]
    [InlineData(200, false)]
    [InlineData(250, true)]
    [InlineData(-10, true)]
    public void Vehicle_Should_Validate_FuelTankCapacity(double fuelTankCapacity, bool shouldFail)
    {
        var vehicle = new Vehicle { Name = "Test Vehicle", EngineSize = 2.0, FuelEfficiency = 5.0, FuelTankCapacity = fuelTankCapacity, Weight = 500 };
        var results = ValidateModel(vehicle);
        AssertValidationResult(results, shouldFail, "Fuel tank capacity must be between");
    }

    #endregion

    #region Name Validation Tests

    [Fact]
    public void Vehicle_Should_Have_Valid_DefaultValues()
    {
        var vehicle = new Vehicle();
        Assert.NotNull(vehicle.Name);
        Assert.Equal(string.Empty, vehicle.Name);
    }

    [Fact]
    public void Vehicle_Should_Require_Name()
    {
        var vehicle = new Vehicle { Name = "" };
        var results = ValidateModel(vehicle);
        Assert.Contains(results, v => v.ErrorMessage.Contains("The Name field is required."));
    }

    [Fact]
    public void Vehicle_Should_Reject_Whitespace_Name()
    {
        var vehicle = new Vehicle { Name = "   " };
        var results = ValidateModel(vehicle);
        Assert.Contains(results, v => v.ErrorMessage.Contains("The Name field is required."));
    }

    #endregion

    #region Weight Validation Tests

    [Fact]
    public void Vehicle_Should_Require_Weight()
    {
        var vehicle = new Vehicle { Name = "Test Vehicle", EngineSize = 2.0, FuelEfficiency = 5.0, FuelTankCapacity = 50 };
        var results = ValidateModel(vehicle);
        Assert.Contains(results, v => v.ErrorMessage.Contains("Vehicle weight must be between"));
    }

    [Theory]
    [InlineData(50, true)]
    [InlineData(100, false)]
    [InlineData(2000, false)]
    [InlineData(2500, true)]
    [InlineData(-10, true)]
    public void Vehicle_Should_Validate_Weight(double weight, bool shouldFail)
    {
        var vehicle = new Vehicle { Name = "Test Vehicle", EngineSize = 2.0, FuelEfficiency = 5.0, FuelTankCapacity = 50, Weight = weight };
        var results = ValidateModel(vehicle);
        AssertValidationResult(results, shouldFail, "Vehicle weight must be between");
    }

    #endregion

    #region Overall Validation Test

    [Fact]
    public void Vehicle_Should_Pass_Validation_When_Valid()
    {
        var vehicle = new Vehicle
        {
            Name = "Test Vehicle",
            EngineSize = 2.0,
            FuelEfficiency = 5.0,
            FuelTankCapacity = 50.0,
            Weight = 500
        };

        var results = ValidateModel(vehicle);
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
