using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinoyGigMarket.Data;
using PinoyGigMarket.Models;

namespace PinoyGigMarket.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;


        public ProjectController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.Users.FirstOrDefaultAsync();
            ViewBag.CurrentUser = currentUser;

            var projects = await _context.Projects
                .Where(p => p.ClientID == currentUser.Id)
                .ToListAsync();

            return View(projects);
        }
    }
}
