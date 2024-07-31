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
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // Handle the case where no users are found
                return View("NoUsers"); // Create a view for this case or handle it differently
            }

            var myProjects = await _context.Projects
                .Where(s => s.ClientID == user.Id)
                .Select(s => new ProjectViewModel
                {
                    ProjectId = s.ProjectId,
                    Title = s.Title,
                    Desc = s.Desc,
                    Status = s.Status,
                    Budget = s.Budget
                })
                .ToListAsync();
            return View(myProjects);
        }
        public async Task<IActionResult> Register()
        {
            var currentUser = await _userManager.Users.FirstOrDefaultAsync();
            ViewBag.CurrentUser = currentUser;
            return View();
        }
    }
}
