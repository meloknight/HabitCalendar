namespace HabitCalendar.Models
{
    public class HabitCompletionViewModel
    {
        public DateOnly UserStartDate { get; set; }
        public string? HabitName { get; set; }
        public List<DateOnly> DateHabitCompleted { get; set; } = null!;
    }
}
