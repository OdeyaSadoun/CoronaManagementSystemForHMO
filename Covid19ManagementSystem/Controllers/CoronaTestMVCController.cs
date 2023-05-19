using Microsoft.AspNetCore.Mvc;

namespace Covid19ManagementSystem.Controllers
{
    public class CoronaTestMVCController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddPositiveTest(int id)
        {
            ViewBag.PersonId = id;
            return View();
        }
    }
}
