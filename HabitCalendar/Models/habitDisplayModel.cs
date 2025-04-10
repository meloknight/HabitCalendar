using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace HabitCalendar.Models
{
    public class HabitDisplayModel
    {
        public int HabitId { get; set; }
        public string? HabitName { get; set; }
        public string? HabitDisplayMethod { get; set; }
        public bool isHabitCompleted { get; set; }

        [MaxLength( 30, ErrorMessage = "Max length is 30 characters" )]
        [RegularExpression( @"^[^\[\]\(\)\{\}]+$", ErrorMessage = "Special characters, such as brackets, are not allowed." )]
        public string? HabitDayValue { get; set; } = string.Empty;

        [MaxLength( 100, ErrorMessage = "Max length is 100 characters" )]
        [RegularExpression( @"^[^\[\]\(\)\{\}]+$", ErrorMessage = "Special characters, such as brackets, are not allowed." )]
        public string? Notes { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public int? HabitDaysCompletedId { get; set; }
        public readonly ReadOnlyCollection<string> monthNames =
            new ReadOnlyCollection<string>( new string[]
            {
                        "Jan", "Feb", "Mar", "Apr", "May", "Jun",
                        "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
            } );
    }
}
