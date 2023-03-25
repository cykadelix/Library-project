using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library_project.Models
{
    public class review
    {
        //Review info
        [Key] 
        public int reviewID { get; set; }
        
        public string evaluation { get; set; }
        public short rating { get; set; }



        [ForeignKey("media")]
        public int mediaId { get; set; }
        public media media { get; set; }


        [ForeignKey("student")]
        public int studentId { get; set; }
        public student student { get; set; }


    }

}
