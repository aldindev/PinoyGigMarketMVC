using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinoyGigMarket.Data;
using PinoyGigMarket.Data.Enum;
using PinoyGigMarket.Models;

namespace PinoyGigMarket.Controllers
{
    public class GigsFreelancerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public GigsFreelancerController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                return View("NoUsers");
            }

            var gigPosts = await _context.Projects
                .Where(s => s.Status == ProjectStatus.Open)
                .Select(s => new ProjectViewModel
                {
                    ProjectId = s.ProjectId,
                    Title = s.Title,
                    Desc = s.Desc,
                    Status = s.Status,
                    Location = s.Location,
                    Budget = s.Budget
                })
                .ToListAsync();
            return View(gigPosts);

        }

        public async Task<IActionResult> MyActiveGigs()
        {
            var user = _userManager.GetUserAsync(User);
            if (user == null)
            {
                return View("No Users");
            }

            var gigPosts = await _context.Projects
                .Where(s => s.Status == ProjectStatus.Open)
                .Select(s => new ProjectViewModel
                {
                    ProjectId = s.ProjectId,
                    Title = s.Title,
                    Desc = s.Desc,
                    Status = s.Status,
                    Location = s.Location,
                    Budget = s.Budget
                })
                .ToListAsync();

            return View(gigPosts);
        }
    }
}
