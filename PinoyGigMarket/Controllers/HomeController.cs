using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinoyGigMarket.Data;
using PinoyGigMarket.Models;
using System.Diagnostics;

namespace PinoyGigMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;




        }

        public IActionResult Index()
        {


            if (!User.Identity.IsAuthenticated)  //CHECK for NULLS Here
            {
                // User is logged in, perform actions for logged-in users
                return RedirectToAction("Register", "Account");
            }
            else
            {
                // User is not logged in, redirect or display login message
            }
            return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
