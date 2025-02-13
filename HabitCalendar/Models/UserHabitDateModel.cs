namespace HabitCalendar.Models
{
    public class UserHabitDateModel
    {

        public string? HabitName { get; set; }
        public List<DateOnly> DateHabitCompleted { get; set; } = null!;
    }
}
