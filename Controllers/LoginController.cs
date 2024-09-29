using SeniorProject.Models; // Importing the Models namespace for the project
using SeniorProject.Services; // Importing the Services namespace for the project
using Microsoft.AspNetCore.Mvc; // Importing ASP.NET Core MVC functionalities
using Microsoft.AspNetCore.Http; // Importing ASP.NET Core HTTP functionalities

namespace SeniorProject.Controllers // Defining the namespace for the Controllers
{
    public class LoginController : Controller // Defining the LoginController that inherits from Controller
    {
        private readonly SecurityService _securityService; // Declaring a SecurityService instance for authentication

        // Constructor to initialize the SecurityService
        public LoginController(SecurityService securityService)
        {
            _securityService = securityService; // Assigning the SecurityService to the private field
        }

        // Action method for the Index view
        public IActionResult Index()
        {
            return View(); // Returning the default view for the Index action
        }

        // Action method to process the login, accepting a UserModel
        [HttpPost] // Specifying that this action responds to POST requests
        public IActionResult ProcessLogin(UserModel user)
        {
            // Validate user credentials using the SecurityService
            if (_securityService.IsValid(user)) // Ensure this method validates credentials correctly
            {
                // Set session variable for the logged-in user
                HttpContext.Session.SetString("UserName", user.UserName); // Storing the username in the session
                return RedirectToAction("Index", "Dashboard"); // Redirecting to the Dashboard controller's Index action
            }
            else
            {
                return RedirectToAction("LoginFailure"); // Redirecting to the LoginFailure action if credentials are invalid
            }
        }

        // Action method for a successful login view
        public IActionResult LoginSuccess()
        {
            return View(); // Returning the default view for successful login
        }

        // Action method for a login failure view
        public IActionResult LoginFailure()
        {
            return View(); // Returning the default view for login failure
        }

        // Action method to handle user logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clearing all session data on logout
            return RedirectToAction("Index", "Home"); // Redirecting to the Home controller's Index action
        }
    }
}