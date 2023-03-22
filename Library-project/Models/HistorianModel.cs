using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_project.Models
{
    public class Historian
    {
        //Historian Name
        public string fName { get; set; }
        public string mName { get; set; }
        public string lName { get; set; }

        //Historian Data
        [Key] public int historianID { get; set; }
        public string expertise { get; set; }
        public string education { get; set; }
        public short age { get; set; }

        //Relationships
        public List<Student> studentsToSee { get; set; }
    }

}
