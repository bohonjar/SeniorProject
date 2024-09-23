using SeniorProject.Models;
using SeniorProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace SeniorProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly SecurityService _securityService;

        public LoginController(SecurityService securityService)
        {
            _securityService = securityService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProcessLogin(UserModel user)
        {
            if (_securityService.IsValid(user)) // Ensure this method validates credentials correctly
            {
                // Set session variable
                HttpContext.Session.SetString("UserName", user.UserName);
                return RedirectToAction("Index", "Dashboard"); // Redirect to Dashboard
            }
            else
            {
                return RedirectToAction("LoginFailure");
            }
        }

        public IActionResult LoginSuccess()
        {
            return View();
        }

        public IActionResult LoginFailure()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear session on logout
            return RedirectToAction("Index", "Home");
        }
    }
}