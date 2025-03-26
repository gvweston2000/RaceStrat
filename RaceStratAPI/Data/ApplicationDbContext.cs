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
    }
}
