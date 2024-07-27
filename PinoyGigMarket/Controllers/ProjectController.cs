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
                .Where(p => p.ClientID == "ac5de9ea-c85a-4c89-8492-5524847315c0")
                .ToListAsync();

            return View(projects);
        }
        public async Task<IActionResult> Register()
        {
            var currentUser = await _userManager.Users.FirstOrDefaultAsync();
            ViewBag.CurrentUser = currentUser;
            return View();
        }
    }
}
