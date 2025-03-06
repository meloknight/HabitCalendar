using System.Collections.ObjectModel;

namespace HabitCalendar.Models
{
    public class CalendarInfoModel
    {
        public List<CalendarDisplayModel> currentWeek { get; set; } = null!;
        public List<CalendarDisplayModel> firstWeek { get; set; } = null!;
        public List<CalendarDisplayModel> remainingWeeks { get; set; } = null!;
        public readonly ReadOnlyCollection<string> monthNames =
            new ReadOnlyCollection<string>( new string[]
            {
                "Jan", "Feb", "Mar", "Apr", "May", "Jun",
                "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
            } );
    }
}
