using HabitCalendar.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HabitCalendar.Controllers
{
    public class BaseController:Controller
    {
        protected readonly ApplicationDbContext _db;
        protected readonly UserManager<IdentityUser> _userManager;

        public BaseController( ApplicationDbContext db, UserManager<IdentityUser> userManager )
        {
            _db = db;
            _userManager = userManager;
        }

        public override void OnActionExecuting( ActionExecutingContext context )
        {
            // Runs before every action in derived controllers
            if ( User.Identity?.IsAuthenticated == true )
            {
                string userId = _userManager.GetUserId( User );
                try
                {
                    var DisplayUserName = _db.ApplicationUsers
                        .Where( u => u.Id == userId )
                        .Select( u => u.DisplayUserName )
                        .FirstOrDefault();

                    ViewData["DisplayUserName"] = DisplayUserName;
                }
                catch ( Exception ex )
                {
                    View( ex );
                }
            }
            base.OnActionExecuting( context );
        }
    }
}
