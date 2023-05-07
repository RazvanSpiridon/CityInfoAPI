using CityInfoAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfoAPI.Context
{
    public class CityInfoDbContext : DbContext
    {
        public CityInfoDbContext(DbContextOptions<CityInfoDbContext> options) : base(options)
        {
            
        }

        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointOfInterests { get; set;} = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasMany(p => p.PointsOfInterest)
                .WithOne(c => c.City);

            modelBuilder.Entity<PointOfInterest>()
                .HasOne(c => c.City)
                .WithMany(p => p.PointsOfInterest)
                .HasForeignKey(ci => ci.CityId);
        }
    }
}
