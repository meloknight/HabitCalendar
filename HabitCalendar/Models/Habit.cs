using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HabitCalendar.Models
{
    public class Habit
    {
        [Key]
        public int HabitId { get; set; }
        [Required]
        [DisplayName( "Habit Name" )]
        [MaxLength( 30 )]
        [RegularExpression( @"^[^.\[\]\(\)]+$", ErrorMessage = "Special characters like brackets and periods are not allowed." )]
        public string HabitName { get; set; }
        [DisplayName( "Habit Description" )]
        public string HabitDescription { get; set; }
        [Required]
        [DisplayName( "Habit Display Method" )]
        public string HabitDisplayMethod { get; set; }

    }
}
