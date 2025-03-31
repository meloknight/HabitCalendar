using HabitCalendar.Data;
using HabitCalendar.Models;
using HabitCalendar.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HabitCalendar.Controllers
{
    [Authorize]
    public class HomeController:Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController( ILogger<HomeController> logger, ApplicationDbContext db, UserManager<IdentityUser> userManager )
        {
            _logger = logger;
            _db = db;
            _userManager = userManager;
        }

        //Action method
        public IActionResult Index()
        {
            string? userId = _userManager.GetUserId( User );

            CalendarInfoModel calendarInfo = new CalendarInfoModel();

            if ( userId != null )
            {
                CalendarLayout cl = new CalendarLayout( _db, userId );
                calendarInfo.currentWeek = cl.currentWeek;
                calendarInfo.firstWeek = cl.firstWeek;
                calendarInfo.remainingWeeks = cl.remainingWeeks;

                return View( calendarInfo );
            }
            else
            {
                calendarInfo = null;
                return View( calendarInfo );
            }
        }

        public IActionResult Privacy()
        {
            var userId = _userManager.GetUserId( User );

            var habits = _db.Habits
                .Where( h => h.ApplicationUserId == userId )
                .ToList();

            return View( habits );
        }




        public IActionResult CalendarDayModify( DateOnly date )
        {
            string? userId = _userManager.GetUserId( User );

            List<Habit> userHabits = _db.Habits
                .Where( h => h.ApplicationUserId == userId )
            //.Select( row => row.HabitId )
                .ToList();

            List<int> habitIds = new List<int>();
            foreach ( Habit habit in userHabits )
            {
                habitIds.Add( habit.HabitId );
            }

            // Fill up a set of HabitDisplayModels
            var chosenHabitDayCompleted = _db.HabitDaysCompleted
                .Where( h => h.DateHabitCompleted == date )
                .ToList();


            return View( date );
        }




        [ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
        public IActionResult Error()
        {
            return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
        }
    }
}
