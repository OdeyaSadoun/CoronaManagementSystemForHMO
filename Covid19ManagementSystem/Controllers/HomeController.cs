using Microsoft.AspNetCore.Mvc;

namespace Covid19ManagementSystem.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddPerson()
        {
            return View("~/Views/Person/AddPerson.cshtml");
        }

        public IActionResult PersonDetails(int id)
        {
            return View("~/Views/Person/Details.cshtml", id);
        }

        public IActionResult HmoPersons()
        {
            return View("~/Views/Person/GetAllPersons.cshtml");
        }

        public IActionResult Statistics()
        {
            return View();
        }
    }
}
