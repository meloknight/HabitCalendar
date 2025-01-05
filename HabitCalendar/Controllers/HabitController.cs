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
            if ( ModelState.IsValid )
            {
                _db.Habits.Add( obj );
                _db.SaveChanges();
                return RedirectToAction( "Index", "Habit" );
            }
            return View();
        }
    }
}
