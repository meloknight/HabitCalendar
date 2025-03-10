using HabitCalendar.Data;
using HabitCalendar.Models;

namespace HabitCalendar.Utility
{
    public class queryDatabase
    {



        public List<Habit> generateHabitList( ApplicationDbContext _db, string userId )
        {
            List<Habit> habitList = _db.Habits
            .Where( h => h.ApplicationUserId == userId )
            .ToList();

            return habitList;
        }
    }
}
