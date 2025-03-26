using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceStratAPI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class RacesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly FuelCalculationService _fuelCalculationService;

    public RacesController(ApplicationDbContext context, FuelCalculationService fuelCalculationService)
    {
        _context = context;
        _fuelCalculationService = fuelCalculationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Race>>> GetRaces()
    {
        return await _context.Races.Include(r => r.Vehicle).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Race>> GetRace(int id)
    {
        var race = await _context.Races.Include(r => r.Vehicle).FirstOrDefaultAsync(r => r.Id == id);

        if (race == null)
        {
            return NotFound();
        }

        return race;
    }

    [HttpPost]
    public async Task<ActionResult<Race>> PostRace(Race race)
    {
        var vehicle = await _context.Vehicles.FindAsync(race.VehicleId);
        if (vehicle == null)
        {
            return NotFound("Vehicle not found.");
        }

        var (totalFuelNeeded, pitStopsRequired) = _fuelCalculationService.CalculateFuelAndPitStops(
            vehicle.FuelEfficiency, 
            race.TrackLength, 
            race.TotalLaps, 
            vehicle.FuelTankCapacity, 
            vehicle.Weight, 
            race.AverageSpeed, 
            race.RaceConditionFactor);

        race.FuelPerLap = totalFuelNeeded / race.TotalLaps;
        race.TotalFuelNeeded = totalFuelNeeded;
        race.PitStopsRequired = (int)pitStopsRequired;

        _context.Races.Add(race);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRace), new { id = race.Id }, race);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutRace(int id, Race race)
    {
        var existingRace = await _context.Races.FindAsync(id);

        if (existingRace == null)
        {
            return NotFound();
        }

        var existingVehicle = await _context.Vehicles.FindAsync(race.VehicleId);

        if (existingVehicle == null)
        {
            return NotFound();
        }

        var (totalFuelNeeded, pitStopsRequired) = _fuelCalculationService.CalculateFuelAndPitStops(
            existingVehicle.FuelEfficiency, 
            existingRace.TrackLength, 
            existingRace.TotalLaps, 
            existingVehicle.FuelTankCapacity, 
            existingVehicle.Weight, 
            existingRace.AverageSpeed,
            existingRace.RaceConditionFactor);

        existingRace.FuelPerLap = totalFuelNeeded / race.TotalLaps;
        existingRace.TotalFuelNeeded = totalFuelNeeded;
        existingRace.PitStopsRequired = (int)pitStopsRequired;

        _context.Entry(existingRace).State = EntityState.Modified;
  
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRace(int id)
    {
        var race = await _context.Races.FindAsync(id);
        if (race == null)
        {
            return NotFound();
        }

        _context.Races.Remove(race);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool RaceExists(int id)
    {
        return _context.Races.Any(e => e.Id == id);
    }
}
