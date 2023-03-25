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

        [ForeignKey("student")] 
        public student student { get; set; }
        public int studentId { get; set; }

        [ForeignKey("media")]
        public int mediaId { get; set; }
        public media media { get; set; }

        
    }

}
