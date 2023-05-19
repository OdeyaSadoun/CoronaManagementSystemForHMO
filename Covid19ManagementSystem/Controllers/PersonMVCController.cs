using Microsoft.AspNetCore.Mvc;

namespace Covid19ManagementSystem.Controllers
{
    public class PersonMVCController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddMember()
        {
            return View();
        }
    }
}
