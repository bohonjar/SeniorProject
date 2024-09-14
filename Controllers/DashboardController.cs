using SeniorProject.Models;
using SeniorProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace SeniorProject.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly SecurityDAO _securityDAO;

        public DashboardController(ILogger<DashboardController> logger, SecurityDAO securityDAO)
        {
            _logger = logger;
            _securityDAO = securityDAO;
        }

        public IActionResult Index()
        {
            string userName = HttpContext.Session.GetString("UserName");
            ViewData["UserName"] = userName;

            return View();
        }
    }
}
