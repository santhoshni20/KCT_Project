using KSI_Project.Interfaces;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using KSI_Project.Repositories;
using KSI_Project.Repository.Implementations;
using KSI_Project.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static KSI_Project.Models.DTOs.AuthDTO;

namespace KSI_Project.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;
        
        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("Authenticate")]
        public IActionResult Authenticate(LoginRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                ViewBag.Error = "Username and password are required";
                return View("Index", request);
            }

            var user = _userRepository.ValidateUser(request.Username, request.Password);

            if (user == null)
            {
                ViewBag.Error = "Invalid username or password";
                return View("Index", request);
            }

            // ✅ Redirect to Home Page after successful login
            return RedirectToAction("Index", "Home");
        }
    }
}
