using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library_project.Models
{
    public class employee
    {
        //Employee Name
        public string fName { get; set; }
        public string mName { get; set; }
        public string lName { get; set; }

        //Employee Data
        [Key] public int employeeID { get; set; }

       
        
        public string position { get; set; }
        public float salary { get; set; }
        public short age { get; set; }

        //Personal Info
        public string eMail { get; set; }
        public string password { get; set; }
        public string homeAddress { get; set; }
        public string phoneNumber { get; set; }


        [ForeignKey("employee")]
        public employee supervisor { get; set; }
        public int supervisorID { get; set; }

        
    }

}
