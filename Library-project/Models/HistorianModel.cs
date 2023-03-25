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
        public string fName { get; set; } = string.Empty;
        public string mName { get; set; } = string.Empty;
        public string lName { get; set; } = string.Empty;

        //Historian Data
        [Key] public int historianID { get; set; }
        public string expertise { get; set; } = string.Empty;
        public string education { get; set; } = string.Empty;
        public short age { get; set; }

        //Relationships
        public List<Student> studentsToSee { get; set; }
    }

}
