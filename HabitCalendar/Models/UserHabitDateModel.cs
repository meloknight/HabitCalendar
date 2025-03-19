namespace HabitCalendar.Models
{
    public class UserHabitDateModel
    {
        public int HabitId { get; set; }
        public string? HabitName { get; set; }
        public string? HabitDisplayMethod { get; set; }
        public List<DateAndHabitCompletionDetailsModel> DateHabitCompletedAndDetails { get; set; } = null!;

    }
}
