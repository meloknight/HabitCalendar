using HabitCalendar.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HabitCalendar.Controllers
{
    public class InfoController:BaseController
    {
        public InfoController( ApplicationDbContext db, UserManager<IdentityUser> userManager ) : base( db, userManager )
        {
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
