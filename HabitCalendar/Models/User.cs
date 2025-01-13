using System.ComponentModel.DataAnnotations;

namespace HabitCalendar.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public DateOnly StartDate { get; set; }

        public ICollection<Habit> Habit { get; set; } = null!;

        public User()
        {
            StartDate = DateOnly.FromDateTime( DateTime.UtcNow );
        }


    }
}
