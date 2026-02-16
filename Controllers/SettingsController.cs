using ksi.Interfaces;
using ksi.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ksi.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Settings()
        {
            return View();
        }
        public IActionResult ActivityLogs()
        {
            return View();
        }
        public IActionResult AccessLevel()
        {
            return View();
        }
    }
}
