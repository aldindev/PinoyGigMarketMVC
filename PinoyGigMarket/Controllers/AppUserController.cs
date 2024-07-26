using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinoyGigMarket.Data;
using PinoyGigMarket.Models;

namespace PinoyGigMarket.Controllers
{
    public class AppUserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AppUserController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.Users.FirstOrDefaultAsync();
            ViewBag.CurrentUser = currentUser;

            var firstUser = _context.Users.FirstOrDefault();
            if (firstUser == null)
            {
                // Handle the case where no users are found
                return View("NoUsers"); // Create a view for this case or handle it differently
            }
            return View(firstUser);
        }
    }
}
