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
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return View("No Users");
            }

            var gigPosts = await _context.Projects
                .Where(s => s.FreelancerID == user.Id && s.Status == ProjectStatus.InProgress)
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

        [HttpPost]
        public async Task<IActionResult> AcceptGig(int projectId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var project = await _context.Projects.FindAsync(projectId);
            if (project == null)
            {
                return NotFound();
            }

            // Associate the project with the freelancer
            project.FreelancerID = user.Id;
            project.Status = ProjectStatus.InProgress;
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            // Redirect to MyActiveGigs action
            return RedirectToAction("MyActiveGigs");
        }


    }
}
