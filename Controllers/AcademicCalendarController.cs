using Microsoft.AspNetCore.Mvc;

namespace YourProject.Controllers
{
    public class AcademicCalendarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
