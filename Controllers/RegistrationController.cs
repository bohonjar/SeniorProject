using SeniorProject.Models; // Importing the Models namespace for the project
using SeniorProject.Services; // Importing the Services namespace for the project
using Microsoft.AspNetCore.Mvc; // Importing ASP.NET Core MVC functionalities

namespace SeniorProject.Controllers // Defining the namespace for the Controllers
{
    public class RegistrationController : Controller // Defining the RegistrationController that inherits from Controller
    {
        private readonly SecurityService _securityService; // Declaring a SecurityService instance for user registration operations

        // Constructor to initialize the SecurityService
        public RegistrationController(SecurityService securityService)
        {
            _securityService = securityService; // Assigning the SecurityService to the private field
        }

        // Action method for the registration Index view
        public IActionResult Index()
        {
            return View(); // Returning the default view for the Index action
        }

        // Action method to handle the registration process, accepting a RegistrationModel
        [HttpPost] // Specifying that this action responds to POST requests
        public IActionResult Register(RegistrationModel model)
        {
            if (ModelState.IsValid) // Checking if the model state is valid
            {
                // Check if the username already exists
                if (_securityService.UsernameExists(model.UserName))
                {
                    ModelState.AddModelError("UserName", "Username already exists."); // Adding an error for duplicate username
                    return View("Index", model); // Returning to the Index view with the model
                }

                // Check if the email already exists
                if (_securityService.EmailExists(model.Email))
                {
                    ModelState.AddModelError("Email", "Email already exists."); // Adding an error for duplicate email
                    return View("Index", model); // Returning to the Index view with the model
                }

                // If both username and email are valid, save the user
                _securityService.SaveUser(model); // Saving the new user
                return View("RegistrationSuccess", model); // Returning the RegistrationSuccess view
            }

            return View("Index", model); // If the model state is invalid, return to the Index view with the model
        }
    }
}
