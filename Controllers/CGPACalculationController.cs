using KSI_Project.Interfaces;
using KSI_Project.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using KSI_Project.Repository;
using System;
using System.Threading.Tasks;

namespace KSI_Project.Controllers
{
    public class CGPACalculationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
