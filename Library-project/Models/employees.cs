
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library_project.Models
{
    public class employees
    {
        //employee Name
        public string fname { get; set; }
        public string mname { get; set; }
        public string lname { get; set; }

        //employee Data
        [Key] public int employeeid { get; set; }

       
        
        public string position { get; set; }
        public float salary { get; set; }
        public short age { get; set; }

        //Personal Info
        public string email { get; set; }
        public string password { get; set; }
        public string homeaddress { get; set; }
        public string phonenumber { get; set; }

        
    }

}
