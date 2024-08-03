using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinoyGigMarket.Data;
using PinoyGigMarket.Models;

namespace PinoyGigMarket.Controllers
{
    [Authorize(Policy = "ClientOnly")]
    public class GigsClientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public GigsClientController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> MyPostedGigs()
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
                    Location = s.Location,
                    Budget = s.Budget
                })
                .ToListAsync();
            return View(myProjects);
        }

        public IActionResult ShowAddGigForm()
        {
            return PartialView("_PostAGig", new ProjectViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> PostAGig(ProjectViewModel gigViewModel)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                var project = new Project
                {
                    Title = gigViewModel.Title,
                    Desc = gigViewModel.Desc,
                    Budget = gigViewModel.Budget,
                    Location = gigViewModel.Location,
                    ClientID = user.Id,
                    Client = user
                };

                _context.Projects.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction("MyPostedGigs");
            }

            return PartialView("_PostAGig", gigViewModel);
        }
    }
}
