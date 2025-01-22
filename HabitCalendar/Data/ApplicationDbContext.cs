/* This section is about configuration and is required to get the database connected and working */

using HabitCalendar.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HabitCalendar.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options ) : base( options )
        {

        }

        public DbSet<Habit> Habits { get; set; } = null!;
        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            base.OnModelCreating( modelBuilder );

            modelBuilder.Entity<Habit>().HasData(
                new Habit { HabitId = 1, HabitName = "Brush Teeth", HabitDescription = "Brush my teeth for 2 minutes", HabitDisplayMethod = "icon" },
                new Habit { HabitId = 2, HabitName = "Workout", HabitDescription = "Workout for at least half an hour", HabitDisplayMethod = "string" },
                new Habit { HabitId = 3, HabitName = "Walk the dog", HabitDescription = "Take the dog out for a half hour walk", HabitDisplayMethod = "icon" }
                );

        }
    }
}
