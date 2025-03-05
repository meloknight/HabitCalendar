namespace HabitCalendar.Models
{
    public class CalendarInfoModel
    {
        public List<CalendarDisplayModel> currentWeek { get; set; } = null!;
        public List<CalendarDisplayModel> firstWeek { get; set; } = null!;
        public List<CalendarDisplayModel> remainingWeeks { get; set; } = null!;
    }
}
