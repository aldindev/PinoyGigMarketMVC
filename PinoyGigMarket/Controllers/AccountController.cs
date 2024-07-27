using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PinoyGigMarket.Models;
using Microsoft.EntityFrameworkCore;

namespace PinoyGigMarket.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser>  _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        // GET: /Account/Login
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var currentUser = await _userManager.Users.FirstOrDefaultAsync();
            ViewBag.CurrentUser = currentUser;
            return View();
        }
        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }
    }


}

