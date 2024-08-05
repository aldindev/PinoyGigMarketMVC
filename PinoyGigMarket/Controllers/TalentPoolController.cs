using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PinoyGigMarket.Data;
using PinoyGigMarket.Models;
using Microsoft.EntityFrameworkCore;


namespace PinoyGigMarket.Controllers
{
    public class TalentPoolController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public TalentPoolController(ApplicationDbContext context, UserManager<AppUser> userManager) 
        {
            _context = context;
            _userManager = userManager;
        
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return View("No Users");
            }

            var talentPool = await _context.Users
                .Where(s => s.UserType == UserType.Freelancer)
                .Include(s => s.Skills)
                .Include(s => s.Projects)
                .ToListAsync();

            return View(talentPool);
        }
    }
}
