using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_project.Models
{
    public class projector
    {
        [Key]
        public int serialNumber { get; set; }

        public string brand { get; set; }
        public string description { get; set; }
        public int lumens { get; set; }
        public bool availibility { get; set; }

        [ForeignKey("media")]
        public int mediaId { get; set; }
        public media media { get; set; }


        public projector()
        {

        }
    }
}
