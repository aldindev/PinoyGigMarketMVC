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

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // Handle the case where no users are found
                return View("NoUsers"); // Create a view for this case or handle it differently
            }
            return View(user);
        }

        public  async Task<IActionResult> MySkills()
        {
            // Get the currently logged-in user
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                // Handle the case where no user is logged in
                return RedirectToAction("Login", "Account");
            }

            // Get the skills of the current logged-in user
            var myskills = await _context.Skills
                .Where(s => s.UserID == currentUser.Id)
                .Include(s => s.User)
                .ToListAsync();

            return View(myskills);
        }

        public IActionResult ShowAddSkillForm()
        {
            return PartialView("_AddSkill");
        }

        [HttpPost]
        public async Task<IActionResult> AddSkill(Skill skill)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                // Handle the case where no user is logged in
                return RedirectToAction("Login", "Account");
            }

            // Set the UserID and User properties
            skill.UserID = currentUser.Id;
            skill.User = currentUser;

            if (ModelState.IsValid)
            {
                _context.Skills.Add(skill);
                await _context.SaveChangesAsync();
                return RedirectToAction("MySkills");
            }

            return PartialView("_AddSkill", skill);
        }
    }
}
