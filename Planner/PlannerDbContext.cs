using Microsoft.EntityFrameworkCore;
using Planner.Models;
using Planner.ViewModels;

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

        public DbSet<GroupGear> GroupGear { get; set; }

        public DbSet<HikerGear> HikerGear { get; set; }

        public DbSet<TripViewModel> TripViewModel { get; set; }

        public DbSet<HikerViewModel> HikerViewModel { get; set; }

        public DbSet<GroupGearViewModel> GroupGearModel { get; set; }

        public DbSet<HikerGearViewModel> HikerGearModel { get; set; }

        public DbSet<PredefinedGroupGear> PredefinedGroupGear { get; set; }

        public DbSet<ParkingPass> ParkingPass { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HikerTrip>()
                .HasKey(ht => ht.Id);

            //modelBuilder.Entity<HikerTrip>()
            //    .HasKey(ht => new { ht.HikerId, ht.TripId });
            
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

