using Microsoft.EntityFrameworkCore;
using Planner.Models;

namespace Planner
{
	public class PlannerDbContext : DbContext
    {
        public PlannerDbContext(DbContextOptions<PlannerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Hiker> Hiker { get; set; }

        public DbSet<Peak> Peak { get; set; }

        public DbSet<Trip> Trip { get; set; }

        public DbSet<TripViewModel> TripViewModel { get; set; }

        public DbSet<HikerViewModel> HikerViewModel { get; set; }
    }
}
