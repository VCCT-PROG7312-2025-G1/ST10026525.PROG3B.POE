using Microsoft.EntityFrameworkCore;
using ST10026525.PROG3B.POE.Models;

namespace ST10026525.PROG3B.POE.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed at least 15 events
            modelBuilder.Entity<Event>().HasData(
                new Event { Id = Guid.NewGuid(), Title = "Community Cleanup", Description = "Bring gloves & bags", Category = "Community", Location = "Greenfield Park", Date = DateTime.Today.AddDays(5) },
                new Event { Id = Guid.NewGuid(), Title = "Farmers Market", Description = "Fresh produce", Category = "Market", Location = "Town Square", Date = DateTime.Today.AddDays(2) },
                new Event { Id = Guid.NewGuid(), Title = "Power Maintenance", Description = "Planned outage", Category = "Maintenance", Location = "Downtown", Date = DateTime.Today.AddDays(7) },
                new Event { Id = Guid.NewGuid(), Title = "Free Health Screening", Description = "All welcome", Category = "Health", Location = "Community Hall", Date = DateTime.Today.AddDays(12) },
                new Event { Id = Guid.NewGuid(), Title = "School Safety Talk", Description = "Parents invited", Category = "Education", Location = "Primary School", Date = DateTime.Today.AddDays(15) },
                new Event { Id = Guid.NewGuid(), Title = "Roadworks Notice", Description = "Expect delays", Category = "Roads", Location = "Main Street", Date = DateTime.Today.AddDays(1) },
                new Event { Id = Guid.NewGuid(), Title = "Public Hearing", Description = "Citizen feedback session", Category = "Public Notice", Location = "City Hall", Date = DateTime.Today.AddDays(21) },
                new Event { Id = Guid.NewGuid(), Title = "Youth Coding Bootcamp", Description = "Free coding classes", Category = "Education", Location = "Tech Lab", Date = DateTime.Today.AddDays(18) },
                new Event { Id = Guid.NewGuid(), Title = "Immunization Drive", Description = "Children's vaccines", Category = "Health", Location = "Clinic", Date = DateTime.Today.AddDays(9) },
                new Event { Id = Guid.NewGuid(), Title = "Street Light Fixes", Description = "Lights being repaired", Category = "Utilities", Location = "Elm Road", Date = DateTime.Today.AddDays(3) },
                new Event { Id = Guid.NewGuid(), Title = "Recycling Info Day", Description = "How to recycle", Category = "Environment", Location = "Library", Date = DateTime.Today.AddDays(8) },
                new Event { Id = Guid.NewGuid(), Title = "Senior Social", Description = "Tea & games", Category = "Community", Location = "Senior Center", Date = DateTime.Today.AddDays(11) },
                new Event { Id = Guid.NewGuid(), Title = "Tree Planting", Description = "Volunteers welcome", Category = "Environment", Location = "Riverside", Date = DateTime.Today.AddDays(6) },
                new Event { Id = Guid.NewGuid(), Title = "Job Fair", Description = "Employers hiring", Category = "Business", Location = "Expo Centre", Date = DateTime.Today.AddDays(14) },
                new Event { Id = Guid.NewGuid(), Title = "Water Notice", Description = "Intermittent supply", Category = "Utilities", Location = "Various Suburbs", Date = DateTime.Today.AddDays(4) }
            );

          
            modelBuilder.Entity<Admin>().HasData(
                new Admin { Id = 1, Username = "admin", Password = "tswaneadmin2025!" } // hashed later
            );
        }
    }
}

