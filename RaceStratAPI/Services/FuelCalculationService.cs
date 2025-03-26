namespace RaceStratAPI.Services
{
    public class FuelCalculationService : IFuelCalculationService
    {
        public double CalculateFuelPerLap(double fuelEfficiency, double trackLength, double vehicleWeight = 0)
        {
            if (fuelEfficiency <= 0)
                throw new ArgumentException("Fuel efficiency must be greater than zero.");
            if (trackLength <= 0)
                throw new ArgumentException("Track length must be greater than zero.");

            double weightModifier = vehicleWeight > 0 ? 1 + (vehicleWeight / 1000) * 0.1 : 1.0;
            return Math.Round((trackLength / fuelEfficiency) * weightModifier, 2);
        }

        public double CalculateTotalFuel(double fuelPerLap, int totalLaps)
        {
            if (totalLaps < 0)
                throw new ArgumentException("Total laps cannot be negative.");

            return fuelPerLap * totalLaps;
        }

        public double PredictPitStops(double totalFuelNeeded, double fuelTankCapacity)
        {
            if (fuelTankCapacity <= 0)
                throw new ArgumentException("Fuel tank capacity must be greater than zero.");
            if (totalFuelNeeded < 0)
                throw new ArgumentException("Total fuel needed cannot be negative.");

            return Math.Ceiling(totalFuelNeeded / fuelTankCapacity);
        }

        public double AdjustFuelForSpeedAndConditions(double fuelPerLap, double averageSpeed, double raceConditionFactor)
        {
            if (averageSpeed <= 0)
                throw new ArgumentException("Average speed must be greater than zero.");
            if (raceConditionFactor <= 0)
                throw new ArgumentException("Race condition factor must be greater than zero.");

            return fuelPerLap * (1 + (averageSpeed / 200) * raceConditionFactor);
        }

        public (double totalFuelNeeded, double pitStopsRequired) CalculateFuelAndPitStops(
            double fuelEfficiency, 
            double trackLength, 
            int totalLaps, 
            double fuelTankCapacity, 
            double vehicleWeight, 
            double averageSpeed, 
            double raceConditionFactor
        )
        {
            if (fuelEfficiency <= 0)
                throw new ArgumentException("Fuel efficiency must be greater than zero.");
            if (trackLength <= 0)
                throw new ArgumentException("Track length must be greater than zero.");
            if (totalLaps <= 0)
                throw new ArgumentException("Total laps must be greater than zero.");
            if (fuelTankCapacity <= 0)
                throw new ArgumentException("Fuel tank capacity must be greater than zero.");
            if (vehicleWeight <= 0)
                throw new ArgumentException("Vehicle weight must be greater than zero.");
            if (averageSpeed <= 0)
                throw new ArgumentException("Average speed must be greater than zero.");
            if (raceConditionFactor <= 0)
                throw new ArgumentException("Race condition factor must be greater than zero.");

            double fuelPerLap = CalculateFuelPerLap(fuelEfficiency, trackLength, vehicleWeight);
            fuelPerLap = AdjustFuelForSpeedAndConditions(fuelPerLap, averageSpeed, raceConditionFactor);
            double totalFuelNeeded = CalculateTotalFuel(fuelPerLap, totalLaps);
            double pitStopsRequired = PredictPitStops(totalFuelNeeded, fuelTankCapacity);

            return (totalFuelNeeded, pitStopsRequired);
        }
    }
}
