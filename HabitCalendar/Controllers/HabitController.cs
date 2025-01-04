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
    }
}
