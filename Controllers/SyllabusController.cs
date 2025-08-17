using Microsoft.AspNetCore.Mvc;

namespace YourProject.Controllers
{
    public class SyllabusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
