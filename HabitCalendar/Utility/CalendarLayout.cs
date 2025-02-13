using HabitCalendar.Data;
using HabitCalendar.Models;

namespace HabitCalendar.Utility
{

    public class CalendarLayout
    {
        // What I want from this?
        // I will return numerous lists or enumerables (should not be modified after creation
        // The objects contained in the lists will provide all required info for a day
        // --- date, all habits and if they were completed or not that day
        // - The First list will be for the current week (must start from Sunday, or
        // earliest day of the week the user has been on the app
        // - The Second list contains the first week (week the user started using the app)
        // This must start from the starting day and go to Sunday (if they have used app
        // long enough)
        // - The Third list contains the remaining days not included in current week and first week
        // - The Fourth List will include the months and help position them to left of calendar

        private readonly ApplicationDbContext _db;
        private readonly string _userId;
        internal readonly DateOnly _userStartDate;
        internal readonly List<UserHabitDateModel> _userHabitDates;
        //private readonly UserManager<IdentityUser> _userManager;

        public CalendarLayout( ApplicationDbContext db, string userId )
        {
            //, UserManager<IdentityUser> userManager
            _db = db;
            _userId = userId;
            //_userManager = userManager;
            _userStartDate = RetrieveUserStartDate();
            _userHabitDates = RetrieveUserHabitDates();

        }

        public DateOnly RetrieveUserStartDate()
        {
            DateOnly userStartDate = _db.ApplicationUsers
                .Where( u => u.Id == _userId )
                .Select( u => u.UserStartDate )
                .First();

            return userStartDate;
        }

        public List<UserHabitDateModel> RetrieveUserHabitDates()
        {
            List<UserHabitDateModel> userHabitDates = new List<UserHabitDateModel>();

            userHabitDates = _db.Habits
                .Where( h => h.ApplicationUserId == _userId )
                .Select( h => new UserHabitDateModel
                {
                    HabitName = h.HabitName,
                    DateHabitCompleted = h.HabitsDaysCompleted.Select( c => c.DateHabitCompleted ).ToList()
                } )
                .ToList();

            return userHabitDates;
        }


    }
}
