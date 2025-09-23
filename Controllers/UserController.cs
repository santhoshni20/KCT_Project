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
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Signup()
        {
            ViewData["NoSidebar"] = true;
            return View();
        }

        [HttpPost]
        public IActionResult Signup(UserSignupDTO signupDTO)
        {
            var response = _userRepository.registerUser(signupDTO);

            if (response.StatusCode == 200)
                return RedirectToAction("Login");

            ViewBag.ErrorMessage = response.Message + (response.ErrorDetails != null ? ": " + response.ErrorDetails : "");
            return View(signupDTO);
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewData["NoSidebar"] = true;
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLoginDTO loginDTO)
        {
            var response = _userRepository.loginUser(loginDTO);

            if (response.StatusCode == 200)
                return RedirectToAction("Index", "Home");

            ViewBag.ErrorMessage = response.Message + (response.ErrorDetails != null ? ": " + response.ErrorDetails : "");
            return View(loginDTO);
        }
    }
}
