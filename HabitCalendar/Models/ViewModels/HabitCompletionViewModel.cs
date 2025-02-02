namespace HabitCalendar.Models.ViewModels
{
    public class HabitCompletionViewModel
    {
        public string? HabitName { get; set; }
        public List<DateOnly> DateHabitCompleted { get; set; } = null!;
    }
}
