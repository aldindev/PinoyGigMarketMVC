using Microsoft.AspNetCore.Mvc;

namespace PinoyGigMarket.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
