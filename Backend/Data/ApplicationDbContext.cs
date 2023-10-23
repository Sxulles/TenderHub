using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IdentityTest.Model;
using IdentityTest.Model.DbEntities;

namespace IdentityTest.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Jobtask> Jobtasks { get; set; }
        public DbSet<Surface> Surfaces { get; set; }
        public DbSet<SavedAdvertisement> SavedAdvertisements { get; set; }

        public ApplicationDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // It would be a good idea to move the connection string to user secrets
            options.UseSqlServer("Server=localhost,1433;Database=TenderHub;User Id=sa;Password=Asd12345!;TrustServerCertificate=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SavedAdvertisement>()
                .HasOne(sa => sa.ApplicationUser)
                .WithMany(au => au.SavedAdvertisements)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
