using Microsoft.AspNetCore.Mvc;

namespace Covid19ManagementSystem.Controllers
{
    public class CoronaVaccineMVCController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddVaccine(int id)
        {
            ViewBag.PersonId = id;
            ViewBag.Manufacturers = new List<string> { "Pfizer", "Moderna", "Johnson & Johnson" };
            return View();
        }
    }
}
