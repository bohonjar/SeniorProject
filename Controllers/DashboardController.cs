using SeniorProject.Models; // Importing the Models namespace for the project
using SeniorProject.Services; // Importing the Services namespace for the project
using Microsoft.AspNetCore.Mvc; // Importing ASP.NET Core MVC functionalities
using Microsoft.Extensions.Logging; // Importing logging functionalities

namespace SeniorProject.Controllers // Defining the namespace for the Controllers
{
    public class DashboardController : Controller // Defining the DashboardController that inherits from Controller
    {
        private readonly ILogger<DashboardController> _logger; // Declaring a logger for the DashboardController
        private readonly SecurityDAO _securityDAO; // Declaring a SecurityDAO instance for security operations

        // Constructor to initialize the logger and SecurityDAO
        public DashboardController(ILogger<DashboardController> logger, SecurityDAO securityDAO)
        {
            _logger = logger; // Assigning the logger to the private field
            _securityDAO = securityDAO; // Assigning the SecurityDAO to the private field
        }

        // Action method for the Index view
        public IActionResult Index()
        {
            string userName = HttpContext.Session.GetString("UserName"); // Retrieving the username from session

            // Check if the user is logged in
            if (string.IsNullOrEmpty(userName)) // If the username is null or empty
            {
                // Redirect to login if not authenticated
                return RedirectToAction("Index", "Login"); // Redirecting to the Login controller's Index action
            }

            ViewData["UserName"] = userName; // Storing the username in ViewData to be accessed in the view
            return View(); // Returning the default view for the Index action
        }
    }
}