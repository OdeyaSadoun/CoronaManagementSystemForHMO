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

        public IActionResult AddPositiveTestToPerson(int id)
        {
            return View("~/Views/CoronaTest/AddPositiveTest.cshtml", id);
        }

        //****************************************************
        public IActionResult SetRecoveryToPerson(int id)
        {
            return View("~/Views/CoronaTest/SetRecovery.cshtml", id);
        }

        public IActionResult AddVaccineToPerson(int id)
        {
            return View("~/Views/CoronaVaccine/AddVaccine.cshtml", id);
        }

        public IActionResult HmoPersons()
        {
            return View("~/Views/Person/GetAllPersons.cshtml");
        }

        public IActionResult Statistics()
        {
            return View("~/Views/CoronaTest/Statistics.cshtml");
        }
    }
}
