using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_project.Models
{
    public class historians
    {
        //Historian Name
        public string fname { get; set; }
        public string mname { get; set; }
        public string lname { get; set; }

        //Historian Data
        [Key] 
        public int historianid { get; set; }
        public string expertise { get; set; }
        public string education { get; set; } 
        public short age { get; set; }

        //Relationships
        public List<students> studentstosee { get; set; }
    }

}
