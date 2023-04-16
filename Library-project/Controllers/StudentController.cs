using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library_project.Data;
using Library_project.Models;
using Library_project.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library_project.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;

        StudentController(IStudentRepository _studentRepository)
        {
            studentRepository = _studentRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<students> students = await studentRepository.GetAll();
            return View(students);
        }
    }
}

