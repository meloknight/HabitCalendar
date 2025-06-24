using HabitCalendar.Data;
using HabitCalendar.Models;
using HabitCalendar.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HabitCalendar.Controllers
{
    public class HabitController:BaseController
    {
        public HabitController( ApplicationDbContext db, UserManager<IdentityUser> userManager ) : base( db, userManager )
        {
        }

        public IActionResult Index()
        {
            string? userId = _userManager.GetUserId( User );

            if ( userId == null )
            {
                string? returnUrl = Url.Action( "Index", "Home" );
                return RedirectToPage( "/Account/Login", new { area = "Identity", returnUrl } );
            }
            try
            {
                queryDatabase qD = new();
                List<Habit> habitList = qD.generateHabitList( _db, userId );

                return base.View( habitList );
            }
            catch ( Exception ex )
            {
                return View( ex );
            }
        }

        public IActionResult Create()
        {
            string? userId = _userManager.GetUserId( User );
            queryDatabase qD = new queryDatabase();
            List<Habit> habitList = qD.generateHabitList( _db, userId );
            List<string> currentHabitDisplays = new List<string>();
            foreach ( Habit habit in habitList )
            {
                currentHabitDisplays.Add( habit.HabitDisplayMethod );
            }
            ViewBag.currentHabitDisplays = currentHabitDisplays;

            return View();
        }
        [HttpPost]
        public IActionResult Create( Habit obj )
        {
            //if ( obj.HabitName.Equals( obj.HabitDescription ) )
            //{
            //    ModelState.AddModelError( "HabitName", "The Habit Name cannot match the Habit Description" );
            //}
            //if ( obj.HabitName != null && obj.HabitName.Equals( "test" ) )
            //{
            //    ModelState.AddModelError( "", "Test is an invalid value" );
            //}
            var userId = _userManager?.GetUserId( User );
            obj.ApplicationUserId = userId;

            if ( ModelState.IsValid )
            {
                _db.Habits.Add( obj );
                _db.SaveChanges();
                TempData["success"] = "Habit created successfully!";
                return RedirectToAction( "Index", "Habit" );
            }
            return View();
        }

        public IActionResult Edit( int? habitId )
        {
            if ( habitId == null || habitId == 0 )
            {
                return NotFound();
            }
            Habit? HabitFromDb = _db.Habits.Find( habitId );
            if ( HabitFromDb == null )
            {
                return NotFound();
            }
            string? userId = _userManager.GetUserId( User );
            queryDatabase qD = new queryDatabase();
            List<Habit> habitList = qD.generateHabitList( _db, userId );
            List<string> currentHabitDisplays = new List<string>();
            foreach ( Habit habit in habitList )
            {
                currentHabitDisplays.Add( habit.HabitDisplayMethod );
            }
            ViewBag.currentHabitDisplays = currentHabitDisplays;

            return View( HabitFromDb );
        }
        [HttpPost]
        public IActionResult Edit( Habit obj )
        {
            var userId = _userManager?.GetUserId( User );
            obj.ApplicationUserId = userId;



            if ( ModelState.IsValid )
            {
                _db.Habits.Update( obj );
                _db.SaveChanges();
                TempData["success"] = "Habit updated successfully!";
                return RedirectToAction( "Index", "Habit" );
            }
            return View();
        }

        public IActionResult Delete( int? habitId )
        {
            if ( habitId == null || habitId == 0 )
            {
                return NotFound();
            }
            Habit? HabitFromDb = _db.Habits.Find( habitId );

            if ( HabitFromDb == null )
            {
                return NotFound();
            }
            return View( HabitFromDb );
        }
        [HttpPost, ActionName( "Delete" )]

        public IActionResult DeletePOST( int? HabitId )
        {
            Habit? obj = _db.Habits.Find( HabitId );
            if ( obj == null )
            {
                return NotFound();
            }
            _db.Habits.Remove( obj );
            _db.SaveChanges();
            TempData["success"] = "Habit deleted successfully!";
            return RedirectToAction( "Index", "Habit" );
        }
    }
}
