using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library_project.Data;
using Library_project.Models;
using Library_project.Interfaces;
using Npgsql;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library_project.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public StudentController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<students> students = await studentRepository.GetAll();
            return View(students);
        }

        public IActionResult StudentCheckouts()
        {
            return View();
        }

        public IActionResult StudentBalance()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PayBalance(int amount) 
        {
            if (ModelState.IsValid)
            {
                using (var conn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))

                {
                    conn.Open();

                    using var cmd = new NpgsqlCommand("UPDATE students SET overduefees -= @a WHERE library_card_number = 5", conn)
                    {
                        Parameters =
                        {
                            new("a", amount),

                        }
                    };
                    using var reader = cmd.ExecuteReader();

                }

            }
            return View("~/Views/Student/StudentBalance.cshtml");

        }
    }
}

