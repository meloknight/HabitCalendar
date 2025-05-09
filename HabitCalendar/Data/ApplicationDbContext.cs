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
        public DbSet<HabitDaysCompleted> HabitDaysCompleted { get; set; } = null!;
        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            base.OnModelCreating( modelBuilder );

            modelBuilder.Entity<ApplicationUser>()
                .HasMany( u => u.Habits )
                .WithOne( h => h.ApplicationUser )
                .HasForeignKey( h => h.ApplicationUserId )
                .OnDelete( DeleteBehavior.Cascade );


        }
    }
}
