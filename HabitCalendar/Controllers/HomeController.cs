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

            if ( userId != null )
            {
                CalendarLayout CL = new CalendarLayout( _db, userId );
            }

            List<HabitCompletionViewModel> userHabitsWithDatesCompleted =
                new List<HabitCompletionViewModel>();
            if ( userId != null )
            {
                CalendarLayout cl = new CalendarLayout( _db, userId );

                var userStartDateList = _db.ApplicationUsers
                    .Where( u => u.Id == userId )
                    .Select( u => u.UserStartDate )
                    .First();
                DateOnly userStartDate = userStartDateList;

                userHabitsWithDatesCompleted = _db.Habits
                    .Where( h => h.ApplicationUserId == userId )
                    .Select( h => new HabitCompletionViewModel
                    {
                        UserStartDate = userStartDate,
                        HabitName = h.HabitName,
                        DateHabitCompleted = h.HabitsDaysCompleted.Select( c => c.DateHabitCompleted ).ToList()
                    } )
                    .ToList();
                return View( userHabitsWithDatesCompleted );
            }
            else
            {
                userHabitsWithDatesCompleted = null;
                return View( userHabitsWithDatesCompleted );
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

        [ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
        public IActionResult Error()
        {
            return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
        }
    }
}
