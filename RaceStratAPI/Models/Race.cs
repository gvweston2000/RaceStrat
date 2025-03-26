using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Race
{
    [Key] 
    public int Id { get; set; }

    [Required]
    public double AverageSpeed { get; set; } = 200.0;

    [Required]
    [Range(1, 200, ErrorMessage = "Total laps must be between 1 and 200.")]
    public int TotalLaps { get; set; }
    
    [Required]
    [Range(1, 10_000, ErrorMessage = "Track length must be between 1 and 10,000 km.")]
    public double TrackLength { get; set; }

    [Required] 
    public string TrackName { get; set; } = string.Empty;

    [Required]
    [Range(0.1, 5.0, ErrorMessage = "Race condition factor must be between 0.1 and 5.0.")]
    public double RaceConditionFactor { get; set; } = 1.0;

    [ForeignKey("Vehicle")] 
    [Range(1, int.MaxValue, ErrorMessage = "VehicleId must be a positive number")]
    public int VehicleId { get; set; }

    public Vehicle? Vehicle { get; set; }

    public double FuelPerLap { get; set; }

    public int PitStopsRequired { get; set; }

    public double TotalFuelNeeded { get; set; }
}
