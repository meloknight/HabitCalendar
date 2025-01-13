/* This section is about configuration and is required to get the database connected and working */

using HabitCalendar.Models;
using Microsoft.EntityFrameworkCore;

namespace HabitCalendar.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options ) : base( options )
        {

        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Habit> Habits { get; set; } = null!;

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, UserName = "Billy123", Password = "1111", StartDate = DateOnly.FromDateTime( DateTime.UtcNow ) },
                new User { UserId = 2, UserName = "Tommers5555", Password = "2222", StartDate = DateOnly.FromDateTime( DateTime.UtcNow ) }
                );
            //modelBuilder.Entity<Habit>().HasData(
            //    new Habit { HabitId = 1, HabitName = "Brush Teeth", HabitDescription = "Brush my teeth for 2 minutes", HabitDisplayMethod = "icon", UserId = 1 },
            //    new Habit { HabitId = 2, HabitName = "Workout", HabitDescription = "Workout for at least half an hour", HabitDisplayMethod = "string", UserId = 2 },
            //    new Habit { HabitId = 3, HabitName = "Walk the dog", HabitDescription = "Take the dog out for a half hour walk", HabitDisplayMethod = "icon", UserId = 1 }
            //    );
        }
    }
}
