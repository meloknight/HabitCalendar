using HabitCalendar.Data;
using HabitCalendar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HabitCalendar.Controllers
{
    public class HabitController:Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public HabitController( ApplicationDbContext db, UserManager<IdentityUser> userManager )
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId( User );
            if ( userId == null )
            {
                string? returnUrl = Url.Action( "Index", "Home" );
                return RedirectToPage( "/Account/Login", new { area = "Identity", returnUrl } );
            }
            List<Habit> habitList = _db.Habits
                .Where( h => h.ApplicationUserId == userId )
                .ToList();

            //List<Habit> objHabitList = _db.Habits.ToList();
            return View( habitList );
        }

        public IActionResult Create()
        {
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
            //Habit? HabitFromDb1 = _db.Habits.FirstOrDefault( u => u.HabitId == habitId );
            //Habit? HabitFromDb2 = _db.Habits.Where( u => u.HabitId == habitId ).FirstOrDefault();
            if ( HabitFromDb == null )
            {
                return NotFound();
            }
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
            //Habit? HabitFromDb1 = _db.Habits.FirstOrDefault( u => u.HabitId == habitId );
            //Habit? HabitFromDb2 = _db.Habits.Where( u => u.HabitId == habitId ).FirstOrDefault();
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
