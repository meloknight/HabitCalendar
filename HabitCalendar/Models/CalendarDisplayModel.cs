namespace HabitCalendar.Models
{
    public class CalendarDisplayModel
    {
        public DateOnly Date { get; set; }
        public List<HabitDisplayModel> habitsCompleted { get; set; }

        public CalendarDisplayModel()
        {
            habitsCompleted = new List<HabitDisplayModel>();
        }
    }
}
