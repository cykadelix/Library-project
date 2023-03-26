using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library_project.Models
{
    public class Review
    {
        //Review info
        [Key] public int reviewID { get; set; }
        public string evaluation { get; set; } = string.Empty;
        public short rating { get; set; }
        [ForeignKey("objectID")] public int objectID { get; set; }


        //Relationships
        public object obj { get; set; }         //object?? is this a media?
        //public Student reviewing_student { get; set; } // We should only have foreign keys to the IDs of these the relationships
    }

}
