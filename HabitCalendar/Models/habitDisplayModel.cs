namespace HabitCalendar.Models
{
    public class HabitDisplayModel
    {
        public int HabitId { get; set; }
        public string? HabitName { get; set; }
        public string? HabitDisplayMethod { get; set; }
        public bool isHabitCompleted { get; set; }
        public string HabitDayValue { get; set; } = string.Empty;
    }
}
