using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library_project.Models
{
    public class checkouts
    {
        //Review info
        public DateTime checkoutdate { get; set; }
        public DateTime returndate { get; set; }
        [Key] public int checkoutid { get; set; }

        [ForeignKey("students")] 
        public students student { get; set; }
        public int studentid { get; set; }

        [ForeignKey("media")]
        public int mediaid { get; set; }
        public medias media { get; set; }

        
    }

}
