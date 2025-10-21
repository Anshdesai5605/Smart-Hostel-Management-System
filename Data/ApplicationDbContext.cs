using Microsoft.EntityFrameworkCore;
using SmartHostelManagementSystem.Models;

namespace SmartHostelManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<MessMenu> MessMenus { get; set; }
        public DbSet<Complaint> Complaints { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Data for initial setup
            modelBuilder.Entity<Room>().HasData(
                new Room { RoomNo = 101, Capacity = 2, OccupiedCount = 2 },
                new Room { RoomNo = 102, Capacity = 3, OccupiedCount = 1 }
            );

            modelBuilder.Entity<Student>().HasData(
                // Admin/Warden account
                new Student { Id = 1, Name = "Ansh Desai", Email = "admin@hostel.com", Password = "admin", RoomNo = 101, IsAdmin = true },
                // Regular student account
                new Student { Id = 2, Name = "John Doe", Email = "john@student.com", Password = "password", RoomNo = 102, IsAdmin = false }
            );

            modelBuilder.Entity<MessMenu>().HasData(
                new MessMenu { Id = 1, DayOfWeek = DayOfWeek.Monday, Breakfast = "Poha", Lunch = "Roti, Sabji, Dal", Dinner = "Khadhi, Khichdi" },
                new MessMenu { Id = 2, DayOfWeek = DayOfWeek.Tuesday, Breakfast = "Upma", Lunch = "Roti, Paneer", Dinner = "Rice, Dal Fry" },
                new MessMenu { Id = 3, DayOfWeek = DayOfWeek.Wednesday, Breakfast = "Idli Sambhar", Lunch = "Puri, Chole", Dinner = "Veg Biryani" }
            );
        }
    }
}