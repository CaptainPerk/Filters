using Filters.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Filters.Controllers
{
    [ViewResultDetails]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Message", "This is the Index action on the Home controller");
        }

        public IActionResult SecondAction()
        {
            return View("Message", "This is the SecondAction action on the Home controller");
        }
    }
}
