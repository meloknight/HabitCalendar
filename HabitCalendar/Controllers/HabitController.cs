using HabitCalendar.Data;
using HabitCalendar.Models;
using Microsoft.AspNetCore.Mvc;

namespace HabitCalendar.Controllers
{
    public class HabitController:Controller
    {
        private readonly ApplicationDbContext _db;
        public HabitController( ApplicationDbContext db )
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Habit> objHabitList = _db.Habits.ToList();
            return View( objHabitList );
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
