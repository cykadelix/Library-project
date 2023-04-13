using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library_project.Models
{
    public class checkout
    {
        //Review info
        public DateTime checkoutdate { get; set; }
        public DateTime returndate { get; set; }
        [Key] public int checkoutid { get; set; }

        [ForeignKey("student")]
        public int studentid { get; set; }
        public student student { get; set; }
        
        [ForeignKey("media")]
        public int mediaid { get; set; }
        public media media { get; set; }

        
    }

}
