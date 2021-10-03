using Microsoft.EntityFrameworkCore;
using Planner.Models;

namespace Planner
{
	public class PlannerDbContext : DbContext /* IdentityDbContext */
    {
        public PlannerDbContext(DbContextOptions<PlannerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Hiker> Hiker { get; set; }

        public DbSet<Peak> Peak { get; set; }

        public DbSet<Trip> Trip { get; set; }
    }
}
