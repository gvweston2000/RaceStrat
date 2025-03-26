using System;
using Xunit;
using RaceStratAPI.Services;

namespace RaceStratAPI.Tests
{
    public class FuelCalculationServiceTests
    {
        private readonly FuelCalculationService _service;

        public FuelCalculationServiceTests()
        {
            _service = new FuelCalculationService();
        }

        private void AssertApproximatelyEqual(double expected, double actual, double tolerance = 0.01)
        {
            Assert.InRange(actual, expected - tolerance, expected + tolerance);
        }

        #region Fuel Calculation Tests

        [Fact]
        public void CalculateFuelPerLap_ReturnsCorrectValue()
        {
            double fuelEfficiency = 2.5;
            double trackLength = 5.5;
            double expectedFuelPerLap = trackLength / fuelEfficiency;

            double result = _service.CalculateFuelPerLap(fuelEfficiency, trackLength);

            AssertApproximatelyEqual(Math.Round(expectedFuelPerLap, 2), Math.Round(result, 2));
        }

        [Fact]
        public void AdjustFuelForSpeedAndConditions_ReturnsCorrectAdjustedValue()
        {
            double averageSpeed = 160.0;
            double fuelPerLap = 10.0;
            double raceConditionFactor = 1.0;
            double expectedAdjustedFuel = fuelPerLap * (1 + (averageSpeed / 200) * raceConditionFactor);

            double adjustedFuelPerLap = _service.AdjustFuelForSpeedAndConditions(fuelPerLap, averageSpeed, raceConditionFactor);

            AssertApproximatelyEqual(Math.Round(expectedAdjustedFuel, 2), Math.Round(adjustedFuelPerLap, 2));
        }

        #endregion

        #region Pit Stop Calculation Tests

        [Fact]
        public void PredictPitStops_ReturnsCorrectPitStops()
        {
            double fuelTankCapacity = 250.0;
            double totalFuelNeeded = 1000.0;
            double expectedPitStops = Math.Ceiling(totalFuelNeeded / fuelTankCapacity);

            double result = _service.PredictPitStops(totalFuelNeeded, fuelTankCapacity);

            Assert.Equal(expectedPitStops, result);
        }

        [Fact]
        public void CalculateFuelAndPitStops_ReturnsCorrectFuelAndPitStopValues()
        {
            double averageSpeed = 160.0;
            double fuelEfficiency = 2.5;
            double fuelTankCapacity = 110.0;
            double raceConditionFactor = 1.0;
            double trackLength = 5.5;
            int totalLaps = 78;
            double vehicleWeight = 746.0;

            double fuelPerLap = trackLength / fuelEfficiency;
            double adjustedFuelPerLap = fuelPerLap * (1 + (averageSpeed / 200) * raceConditionFactor);
            double expectedTotalFuelNeeded = adjustedFuelPerLap * totalLaps;
            double expectedPitStopsRequired = Math.Ceiling(expectedTotalFuelNeeded / fuelTankCapacity);

            var result = _service.CalculateFuelAndPitStops(
                fuelEfficiency, 
                trackLength, 
                totalLaps, 
                fuelTankCapacity, 
                vehicleWeight, 
                averageSpeed, 
                raceConditionFactor
            );

            Assert.InRange(Math.Round(result.totalFuelNeeded, 2), expectedTotalFuelNeeded - 30, expectedTotalFuelNeeded + 30);
            Assert.InRange(Math.Round(result.pitStopsRequired, 2), expectedPitStopsRequired - 1, expectedPitStopsRequired + 1);
        }

        #endregion

        #region Error Handling Tests

        [Fact]
        public void CalculateFuelPerLap_HandlesNegativeFuelEfficiency_ThrowsArgumentException()
        {
            double fuelEfficiency = -2.5;
            double trackLength = 5.5;

            Assert.Throws<ArgumentException>(() => _service.CalculateFuelPerLap(fuelEfficiency, trackLength));
        }

        [Fact]
        public void CalculateFuelPerLap_HandlesZeroTrackLength_ThrowsArgumentException()
        {
            double fuelEfficiency = 2.5;
            double trackLength = 0.0;

            Assert.Throws<ArgumentException>(() => _service.CalculateFuelPerLap(fuelEfficiency, trackLength));
        }

        [Fact]
        public void CalculateFuelPerLap_HandlesNegativeVehicleWeight()
        {
            double fuelEfficiency = 2.5;
            double trackLength = 5.5;
            double vehicleWeight = -1000.0;
            double expectedFuelPerLap = trackLength / fuelEfficiency;

            double result = _service.CalculateFuelPerLap(fuelEfficiency, trackLength, vehicleWeight);

            AssertApproximatelyEqual(Math.Round(expectedFuelPerLap, 2), Math.Round(result, 2));
        }

        [Fact]
        public void CalculateFuelAndPitStops_ThrowsExceptionWhenInvalidArguments()
        {
            double averageSpeed = 160.0;
            double fuelEfficiency = 2.5;
            double fuelTankCapacity = 110.0;
            double raceConditionFactor = 1.0;
            double trackLength = 5.5;
            int totalLaps = -50;
            double vehicleWeight = 746.0;

            Assert.Throws<ArgumentException>(() =>
                _service.CalculateFuelAndPitStops(fuelEfficiency, trackLength, totalLaps, fuelTankCapacity, vehicleWeight, averageSpeed, raceConditionFactor));
        }

        [Fact]
        public void PredictPitStops_HandlesZeroFuelTankCapacity_ThrowsArgumentException()
        {
            double totalFuelNeeded = 1000.0;
            double fuelTankCapacity = 0.0;

            Assert.Throws<ArgumentException>(() => _service.PredictPitStops(totalFuelNeeded, fuelTankCapacity));
        }

        #endregion
    }
}
