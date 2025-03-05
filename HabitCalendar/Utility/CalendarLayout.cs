using HabitCalendar.Data;
using HabitCalendar.Models;
using System.Diagnostics;

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

        private readonly ApplicationDbContext _db;
        private readonly string _userId;
        internal readonly DateOnly _userStartDate;
        internal readonly List<UserHabitDateModel> _userHabitDates;
        internal readonly List<DateOnly> allDates;
        internal readonly List<CalendarDisplayModel> allDatesWithDetails;
        internal readonly List<CalendarDisplayModel> currentWeek;
        internal List<CalendarDisplayModel> firstWeek;
        private readonly bool isCurrentWeekEqualToFirstWeek;
        private readonly bool isCurrentWeekDirectlyAfterFirstWeek;
        internal readonly List<CalendarDisplayModel> remainingWeeks;

        public CalendarLayout( ApplicationDbContext db, string userId )
        {
            _db = db;
            _userId = userId;
            _userStartDate = RetrieveUserStartDate();
            _userHabitDates = RetrieveUserHabitDates();
            allDates = CreateListOfAllDates( _userStartDate );
            allDatesWithDetails = CreateListOfDatesWithDetails( allDates, _userHabitDates );
            currentWeek = CreateCurrentWeekListOfDatesWithDetails( allDatesWithDetails );
            firstWeek = CreateFirstWeekListOfDatesWithDetails( allDatesWithDetails );
            isCurrentWeekEqualToFirstWeek = checkIfCurrentWeekAndFirstWeekAreSame( currentWeek, firstWeek );
            isCurrentWeekDirectlyAfterFirstWeek = checkIfCurrentWeekDirectlyAfterFirstWeek( currentWeek, firstWeek );
            remainingWeeks = CreateRemainingWeekListOfDatesWithDetails(
                allDatesWithDetails, firstWeek, currentWeek, isCurrentWeekEqualToFirstWeek, isCurrentWeekDirectlyAfterFirstWeek );
            updateFirstWeekIfCurrentWeekEqualToFirstWeek( firstWeek, isCurrentWeekEqualToFirstWeek );
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
                CalendarDisplayModel tempDate = CreateInvisibleDateForCalendar( tempDateDecrementer );
                currentWeek.Insert( 0, tempDate );
            }
            return currentWeek;
        }
        public List<CalendarDisplayModel> CreateFirstWeekListOfDatesWithDetails( List<CalendarDisplayModel> allDatesWithDetails )
        {
            List<CalendarDisplayModel> firstWeek = new List<CalendarDisplayModel>();
            int allDatesWithDetailsCountOrSevenDays = Math.Min( allDatesWithDetails.Count, 7 );
            int lastIndexOfAllDatesWithDetails = allDatesWithDetails.Count - 1;

            for ( int i = lastIndexOfAllDatesWithDetails; i > lastIndexOfAllDatesWithDetails - allDatesWithDetailsCountOrSevenDays; i-- )
            {
                // grab from the last index and decrement until Saturday is hit, then break.
                firstWeek.Add( allDatesWithDetails[i] );
                if ( allDatesWithDetails[i].Date.DayOfWeek == DayOfWeek.Saturday ) { break; }
            }
            // fill the remainder of firstWeek with invisible Dates in sequence.
            DateOnly tempDateDecrementer = firstWeek[0].Date;
            while ( tempDateDecrementer.DayOfWeek != DayOfWeek.Sunday )
            {
                tempDateDecrementer = tempDateDecrementer.AddDays( -1 );
                CalendarDisplayModel tempDate = CreateInvisibleDateForCalendar( tempDateDecrementer );
                firstWeek.Insert( 0, tempDate );
            }
            int firstWeekFinalIndex = firstWeek.Count - 1;
            DateOnly tempDateIncrementer = firstWeek[firstWeekFinalIndex].Date;
            while ( firstWeek.Count < 7 )
            {
                tempDateIncrementer = tempDateIncrementer.AddDays( 1 );
                CalendarDisplayModel tempDate = CreateInvisibleDateForCalendar( tempDateIncrementer );
                firstWeek.Add( tempDate );
            };

            return firstWeek;
        }
        private bool checkIfCurrentWeekAndFirstWeekAreSame( List<CalendarDisplayModel> currentWeek, List<CalendarDisplayModel> firstWeek )
        {
            bool isCurrentWeekEqualToFirstWeek = false;
            Debug.Assert( currentWeek.Count == 7, "currentWeek does not have 7 CalendarDisplayModels in it." );
            Debug.Assert( firstWeek.Count == 7, "firstWeek does not have 7 CalendarDisplayModels in it." );
            for ( int i = 0; i < 7; i++ )
            {
                if ( currentWeek[i] == firstWeek[i] )
                {
                    isCurrentWeekEqualToFirstWeek = true;
                }
            }
            return isCurrentWeekEqualToFirstWeek;
        }
        private bool checkIfCurrentWeekDirectlyAfterFirstWeek( List<CalendarDisplayModel> currentWeek, List<CalendarDisplayModel> firstWeek )
        {
            bool isCurrentWeekDirectlyAfterFirstWeek = false;
            DateOnly tempDate = firstWeek[6].Date;
            tempDate = tempDate.AddDays( 1 );
            if ( currentWeek[0].Date == tempDate ) { isCurrentWeekDirectlyAfterFirstWeek = true; }
            return isCurrentWeekDirectlyAfterFirstWeek;
        }
        private List<CalendarDisplayModel> CreateRemainingWeekListOfDatesWithDetails(
            List<CalendarDisplayModel> allDatesWithDetails,
            List<CalendarDisplayModel> firstWeek,
            List<CalendarDisplayModel> currentWeek,
            bool isCurrentWeekEqualToFirstWeek,
            bool isCurrentWeekDirectlyAfterFirstWeek )
        {
            List<CalendarDisplayModel> remainingWeeks = new List<CalendarDisplayModel>();
            if ( isCurrentWeekEqualToFirstWeek == true ) { return remainingWeeks; }
            if ( isCurrentWeekDirectlyAfterFirstWeek == true ) { return remainingWeeks; }
            DateOnly startDate = currentWeek[0].Date;
            DateOnly endDate = firstWeek[6].Date;
            bool isDateAddable = false;
            foreach ( CalendarDisplayModel day in allDatesWithDetails )
            {
                if ( day.Date == endDate ) { isDateAddable = false; }
                if ( isDateAddable == true )
                {
                    remainingWeeks.Add( day );
                }
                if ( day.Date == startDate ) { isDateAddable = true; }
            }

            // reverse each 7 days so they are laid out correctly for display.
            List<CalendarDisplayModel> remainingWeeksCorrected = new List<CalendarDisplayModel>();
            int weekCount = remainingWeeks.Count / 7;
            for ( int i = 0; i < weekCount; i++ )
            {
                List<CalendarDisplayModel> tempDateList = remainingWeeks.GetRange( i * 7, 7 );
                tempDateList.Reverse();
                remainingWeeksCorrected.AddRange( tempDateList );
            }

            return remainingWeeksCorrected;
        }
        private void updateFirstWeekIfCurrentWeekEqualToFirstWeek( List<CalendarDisplayModel> firstWeek, bool isCurrentWeekEqualToFirstWeek )
        {
            if ( isCurrentWeekEqualToFirstWeek == true ) { firstWeek = new List<CalendarDisplayModel>(); }
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
