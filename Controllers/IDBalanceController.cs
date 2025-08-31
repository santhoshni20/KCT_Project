using Microsoft.AspNetCore.Mvc;
using KSI_Project.Models.Entity;
using KSI_Project.Models.DTOs;
using KSI_Project.Interfaces;
using KSI_Project.Repositories;

namespace KSI_Project.Controllers
{
    public class IDBalanceController : Controller
    {
        private readonly IIDBalanceRepository _repository;

        public IDBalanceController(IIDBalanceRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetDetails(string rollNo)
        {
            if (string.IsNullOrEmpty(rollNo))
            {
                return Json(new { success = false, message = "Roll Number is required" });
            }

            var student = await _repository.GetStudentByRollNoAsync(rollNo);

            if (student == null)
            {
                return Json(new { success = false, message = "Student not found" });
            }

            return Json(new
            {
                success = true,
                data = new
                {
                    name = student.Name,
                    balance = student.Balance,
                    billsHistory = student.Transactions.Select(t => new
                    {
                        amount = t.Amount,
                        date = t.Date.ToString("yyyy-MM-dd HH:mm")
                    }).ToList()
                }
            });
        }
    }
}
