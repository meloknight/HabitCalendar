using System.ComponentModel.DataAnnotations;

namespace HabitCalendar.Models
{
    public class Habit
    {
        [Key]
        public int HabitId { get; set; }
        [Required]
        public string HabitName { get; set; }

        public string HabitDescription { get; set; }
        [Required]
        public string HabitDisplayMethod { get; set; }

    }
}