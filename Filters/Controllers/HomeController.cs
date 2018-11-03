using Filters.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Filters.Controllers
{
    [TypeFilter(typeof(DiagnosticsFilter))]
    [TypeFilter(typeof(TimeFilter))]
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

        public ViewResult GenerateException(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            if (id > 10) throw new ArgumentOutOfRangeException(nameof(id));

            return View("Message", $"The value of id is {id}");
        }
    }
}
