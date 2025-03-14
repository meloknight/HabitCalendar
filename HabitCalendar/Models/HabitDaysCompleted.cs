using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace HabitCalendar.Models
{
    public class HabitDaysCompleted
    {
        [Key]
        public int HabitDaysCompletedId { get; set; }
        [Required]
        public DateOnly DateHabitCompleted { get; set; }
        [MaxLength( 400 )]
        public string HabitDayValue { get; set; } = string.Empty;
        [MaxLength( 400 )]
        public string? Notes { get; set; }
        [Required]
        [ForeignKey( "Habit" )]
        public int HabitId { get; set; }
    }
}
