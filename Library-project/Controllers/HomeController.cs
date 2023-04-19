using Library_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Text;
using Npgsql;
using System;
using System.IO;
using System.Diagnostics;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;

namespace Library_project.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        

        
        
        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            string role = (string)TempData.Peek("role");
            if (string.IsNullOrEmpty(role))
            {
                role = "guest";
                TempData["role"] = role;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult AdminDashboard()
        {
            return View();
        }

        public IActionResult UserDashboard()
        {
            return View();
        }

        public IActionResult AddMediaForm()
        {
            return View();
        }

        public IActionResult Explore() 
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

