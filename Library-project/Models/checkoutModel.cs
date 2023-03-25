using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library_project.Models
{
    public class Checkout
    {
        //Review info
        public DateTime checkoutDate { get; set; }
        public DateTime returnDate { get; set; }
        [Key] public int checkoutID { get; set; }
        [ForeignKey("studentID")] public int studentID { get; set; }
        [ForeignKey("objectID")] public int objectID { get; set; }
    }

}
