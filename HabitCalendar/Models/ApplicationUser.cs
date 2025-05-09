using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HabitCalendar.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public DateOnly UserStartDate { get; set; } = DateOnly.FromDateTime( DateTime.UtcNow );

        public string DisplayUserName { get; set; } = string.Empty;

        public ICollection<Habit> Habits { get; set; } = null!;
    }
}
