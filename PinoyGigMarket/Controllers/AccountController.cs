using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PinoyGigMarket.Models;
using Microsoft.EntityFrameworkCore;


namespace PinoyGigMarket.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser>  _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Password match and other validations passed
                // Process registration logic here

                var user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Phone = model.Phone
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }


            }

            // Model is invalid, return the view with error messages
            return View(model);
        }
    }


}

