using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library_project.Models
{
    public class Employee
    {
        //Employee Name
        public string fName { get; set; } = string.Empty;
        public string mName { get; set; } = string.Empty;
        public string lName { get; set; } = string.Empty;

        //Employee Data
        [Key] public int employeeID { get; set; }
        public int supervisorID { get; set; }
        [ForeignKey("supervisorID")]
        public string position { get; set; } = string.Empty;
        public float salary { get; set; }
        public short age { get; set; }

        //Personal Info
        public string eMail { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string homeAddress { get; set; } = string.Empty;
        public string phoneNumber { get; set; } = string.Empty;

        //Relationships
        //public Student studentToSignUp { get; set; }
    }

}
