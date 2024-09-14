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
                _securityService.SaveUser(model);

                return View("RegistrationSuccess", model);
            }

            return View("Index", model);
        }
    }
}