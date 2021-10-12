using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
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

        public DbSet<HikerTrip> HikerTrip { get; set; }

        public DbSet<TripViewModel> TripViewModel { get; set; }

        public DbSet<HikerViewModel> HikerViewModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HikerTrip>()
                .HasKey(ht => new { ht.HikerId, ht.TripId });
            
            modelBuilder.Entity<HikerTrip>()
                .HasOne(ht => ht.Hiker)
                .WithMany(h => h.HikerTrips)
                .HasForeignKey(ht => ht.TripId);
            
            modelBuilder.Entity<HikerTrip>()
                .HasOne(ht => ht.Trip)
                .WithMany(t => t.HikerTrips)
                .HasForeignKey(bc => bc.HikerId);

        }
    }
}

