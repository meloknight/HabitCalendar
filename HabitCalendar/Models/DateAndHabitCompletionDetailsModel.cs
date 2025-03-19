namespace HabitCalendar.Models
{
    public class DateAndHabitCompletionDetailsModel
    {
        public DateOnly DateHabitCompleted { get; set; }
        public string HabitDayValue { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}
