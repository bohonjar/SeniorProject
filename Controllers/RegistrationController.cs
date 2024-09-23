using SeniorProject.Models;
using SeniorProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace SeniorProject.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly SecurityService _securityService;

        public RegistrationController(SecurityService securityService)
        {
            _securityService = securityService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the username already exists
                if (_securityService.UsernameExists(model.UserName))
                {
                    ModelState.AddModelError("UserName", "Username already exists.");
                    return View("Index", model);
                }

                // Check if the email already exists
                if (_securityService.EmailExists(model.Email))
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                    return View("Index", model);
                }

                // If both username and email are valid, save the user
                _securityService.SaveUser(model);
                return View("RegistrationSuccess", model);
            }

            return View("Index", model);
        }
    }
}