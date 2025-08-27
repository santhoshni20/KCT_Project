using KCT_Project.Interfaces;
using KSI_Project.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KCT_Project.Controllers
{
    public class IDBalanceController : Controller
    {
        //private readonly IIDBalanceRepository _eventRepo;

        //public IDBalanceController(IIDBalanceRepository eventRepo)
        //{
        //    _eventRepo = eventRepo;
        //}

        public IActionResult Index()
        {
            return View();
        }
    }
}
