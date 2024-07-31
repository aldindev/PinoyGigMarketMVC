using Microsoft.AspNetCore.Mvc;

namespace PinoyGigMarket.Controllers
{
    public class GigsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
