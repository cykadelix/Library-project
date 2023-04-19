using Library_project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_project.ViewModels.Student
{
    public class listStudentViewModel
    {
        public List<students> allStudents { get; set; } = new List<students>();
    }

}


