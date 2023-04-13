using Library_project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_project.ViewModels.Employee
{
    public class listEmployeeViewModel
    {
        public List<employee> allEmployees { get; set; } = new List<employee>();
    }

}
