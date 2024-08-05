using Microsoft.AspNetCore.Mvc;

namespace PinoyGigMarket.Controllers
{
    public class MessagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
