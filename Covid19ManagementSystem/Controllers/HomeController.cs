using Microsoft.AspNetCore.Mvc;

namespace Covid19ManagementSystem.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddMember()
        {
            return View("~/Views/Person/AddMember.cshtml");
        }

        public IActionResult HmoMembers()
        {
            return RedirectToAction("Index", "PersonMVC");
        }

        public IActionResult Statistics()
        {
            return View();
        }
    }
}
