using System.ComponentModel.DataAnnotations;

public class Vehicle
{
    [Key] 
    public int Id { get; set; }

    [Required]
    [Range(1.0, 10.0, ErrorMessage = "Engine size must be between 1.0 and 10.0 liters.")]
    public double EngineSize { get; set; }

    [Required]
    [Range(0.1, 100.0, ErrorMessage = "Fuel efficiency must be between 0.1 and 100.0 liters per lap.")]
    public double FuelEfficiency { get; set; }

    [Required]
    [Range(1, 200, ErrorMessage = "Fuel tank capacity must be between 1 and 200 liters.")]
    public double FuelTankCapacity { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(100, 2000, ErrorMessage = "Vehicle weight must be between 100 kg and 2000 kg.")]
    public double Weight { get; set; }
}
