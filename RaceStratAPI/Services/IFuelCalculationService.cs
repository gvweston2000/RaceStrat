namespace RaceStratAPI.Services
{
    public interface IFuelCalculationService
    {
        double CalculateFuelPerLap(double fuelEfficiency, double trackLength, double vehicleWeight);
        double CalculateTotalFuel(double fuelPerLap, int totalLaps);
        double PredictPitStops(double totalFuelNeeded, double fuelTankCapacity);
        double AdjustFuelForSpeedAndConditions(double fuelPerLap, double averageSpeed, double raceConditionFactor);
        (double totalFuelNeeded, double pitStopsRequired) CalculateFuelAndPitStops(
            double fuelEfficiency, 
            double trackLength, 
            int totalLaps, 
            double fuelTankCapacity, 
            double vehicleWeight, 
            double averageSpeed, 
            double raceConditionFactor
        );
    }
}
