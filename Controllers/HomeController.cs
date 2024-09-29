using Microsoft.AspNetCore.Mvc; // Importing ASP.NET Core MVC functionalities
using SeniorProject.Models; // Importing the Models namespace for the project
using System.Diagnostics; // Importing diagnostic functionalities for error handling

namespace SeniorProject.Controllers // Defining the namespace for the Controllers
{
    public class HomeController : Controller // Defining the HomeController that inherits from Controller
    {
        private readonly ILogger<HomeController> _logger; // Declaring a logger for the HomeController

        // Constructor to initialize the logger
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger; // Assigning the logger to the private field
        }

        // Action method for the Index view
        public IActionResult Index()
        {
            return View(); // Returning the default view for the Index action
        }

        // Action method for the Privacy view
        public IActionResult Privacy()
        {
            return View(); // Returning the default view for the Privacy action
        }

        // Action method for handling errors with caching attributes
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] // Specifying response caching options
        public IActionResult Error()
        {
            // Returning the Error view with the current request ID or trace identifier
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}