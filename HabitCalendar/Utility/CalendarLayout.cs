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
        internal List<DateOnly> allDates;
        internal List<CalendarDisplayModel> allDatesWithDetails;
        internal List<CalendarDisplayModel> currentWeek;
        internal List<CalendarDisplayModel> firstWeek;

        public CalendarLayout( ApplicationDbContext db, string userId )
        {
            _db = db;
            _userId = userId;
            _userStartDate = RetrieveUserStartDate();
            _userHabitDates = RetrieveUserHabitDates();
            allDates = CreateListOfAllDates( _userStartDate );
            allDatesWithDetails = CreateListOfDatesWithDetails( allDates, _userHabitDates );
            currentWeek = CreateCurrentWeekListOfDatesWithDetails( allDatesWithDetails );
            //firstWeek = CreateFirstWeekListOfDatesWithDetails( allDatesWithDetails );
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
                    HabitId = h.HabitId,
                    HabitName = h.HabitName,
                    HabitDisplayMethod = h.HabitDisplayMethod,
                    DateHabitCompleted = h.HabitsDaysCompleted.Select( c => c.DateHabitCompleted ).ToList()
                } )
                .ToList();
            return userHabitDates;
        }
        public List<DateOnly> CreateListOfAllDates( DateOnly userStartDate )
        {
            DateOnly currentDate = DateOnly.FromDateTime( DateTime.Today );
            List<DateOnly> listOfAllDates = new List<DateOnly>();
            DateOnly dateIncrementer = userStartDate;
            while ( dateIncrementer != currentDate )
            {
                listOfAllDates.Add( dateIncrementer );
                dateIncrementer = dateIncrementer.AddDays( 1 );
            }
            listOfAllDates.Add( currentDate );
            listOfAllDates.Reverse();
            return listOfAllDates;
        }
        public List<CalendarDisplayModel> CreateListOfDatesWithDetails( List<DateOnly> allDates, List<UserHabitDateModel> userHabitDates )
        {
            List<CalendarDisplayModel> allDatesWithDetails = new List<CalendarDisplayModel>();

            foreach ( DateOnly date in allDates )
            {
                CalendarDisplayModel dateInfo = new CalendarDisplayModel();
                dateInfo.Date = date;
                dateInfo.isVisible = true;

                // want to make a list of all habits along with their info
                foreach ( UserHabitDateModel habit in userHabitDates )
                {
                    HabitDisplayModel habitDisplay = new HabitDisplayModel();
                    habitDisplay.HabitId = habit.HabitId;
                    habitDisplay.HabitName = habit.HabitName;
                    habitDisplay.HabitDisplayMethod = habit.HabitDisplayMethod;
                    habitDisplay.isHabitCompleted = false;

                    foreach ( DateOnly dateHabitCompleted in habit.DateHabitCompleted )
                    {
                        if ( dateHabitCompleted == date )
                        {
                            habitDisplay.isHabitCompleted = true;
                        }
                    }

                    if ( habitDisplay != null )
                    {
                        dateInfo.habitsCompleted.Add( habitDisplay );
                    }

                }
                allDatesWithDetails.Add( dateInfo );
            }
            return allDatesWithDetails;
        }
        public List<CalendarDisplayModel> CreateCurrentWeekListOfDatesWithDetails( List<CalendarDisplayModel> allDatesWithDetails )
        {
            // currentWeek will always contain 7 days (even if week is incomplete or
            // user has not been on app for a full week)
            List<CalendarDisplayModel> currentWeek = new List<CalendarDisplayModel>();
            foreach ( CalendarDisplayModel detailedDate in allDatesWithDetails )
            {
                currentWeek.Add( detailedDate );
                if ( detailedDate.Date.DayOfWeek == DayOfWeek.Sunday ) { break; }
            }
            // grab the furthest day into the week to increment the remaining weekdays from
            DateOnly tempDateIncrementer = currentWeek[0].Date;
            currentWeek.Reverse();
            // Add remaining invisible days for the week
            while ( tempDateIncrementer.DayOfWeek != DayOfWeek.Saturday )
            {
                tempDateIncrementer = tempDateIncrementer.AddDays( 1 );
                CalendarDisplayModel tempDate = CreateInvisibleDateForCalendar( tempDateIncrementer );
                currentWeek.Add( tempDate );
            }
            DateOnly tempDateDecrementer = currentWeek[0].Date;
            while ( currentWeek.Count < 7 )
            {
                tempDateDecrementer = tempDateDecrementer.AddDays( -1 );
                CalendarDisplayModel tempDate = CreateInvisibleDateForCalendar( tempDateIncrementer );
                currentWeek.Insert( 0, tempDate );
            }
            return currentWeek;
        }

        private CalendarDisplayModel CreateInvisibleDateForCalendar( DateOnly tempDateIncrementer )
        {
            CalendarDisplayModel tempDate = new CalendarDisplayModel();
            tempDate.isVisible = false;
            tempDate.habitsCompleted = new List<HabitDisplayModel>();
            tempDate.Date = tempDateIncrementer;
            return tempDate;
        }
    }
}
