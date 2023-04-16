using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library_project.Models
{
    public class reviews
    {
        //Review info
        [Key] 
        public int reviewid { get; set; }
        
        public string evaluation { get; set; }
        public short rating { get; set; }



        [ForeignKey("media")]
        public int mediaid { get; set; }
        public medias media { get; set; }


        [ForeignKey("student")]
        public int studentid { get; set; }
        public students student { get; set; }


    }

}
