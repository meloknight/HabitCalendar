using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string HabitDisplayMethod { get; set; } = null!;

        public DateOnly CreatedDate = DateOnly.FromDateTime( DateTime.UtcNow );

        [ForeignKey( "ApplicationUser" )]
        public string? ApplicationUserId { get; set; }
        //public ApplicationUser ApplicationUser { get; set; } = null!;
        public ICollection<HabitDaysCompleted> HabitsDaysCompleted { get; set; } = null!;
    }
}
