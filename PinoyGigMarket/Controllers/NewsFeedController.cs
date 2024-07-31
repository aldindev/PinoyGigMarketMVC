using Microsoft.AspNetCore.Mvc;

namespace PinoyGigMarket.Controllers
{
    public class NewsFeedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
