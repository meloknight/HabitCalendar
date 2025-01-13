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
        [MaxLength( 30, ErrorMessage = "Max length is 30 characters" )]
        [RegularExpression( @"^[^.\[\]\(\)\{\}]+$", ErrorMessage = "Special characters like brackets and periods are not allowed." )]
        public string? HabitName { get; set; }

        [DisplayName( "Habit Description" )]
        [MaxLength( 120, ErrorMessage = "Max length is 120 characters" )]
        [RegularExpression( @"^[^.\[\]\(\)\{\}\<\>]+$", ErrorMessage = "Special characters like brackets and periods are not allowed." )]
        public string? HabitDescription { get; set; }

        [Required]
        [DisplayName( "Habit Display Method" )]
        public string HabitDisplayMethod { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
