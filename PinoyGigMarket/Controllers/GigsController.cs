using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PinoyGigMarket.Controllers
{
    [Authorize(Policy = "ClientOnly")]
    public class GigsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
