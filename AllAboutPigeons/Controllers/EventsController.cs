using Microsoft.AspNetCore.Mvc;

namespace AllAboutPigeons.Controllers
{
    public class EventsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
