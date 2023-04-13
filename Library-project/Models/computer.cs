using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_project.Models
{
    public class computer
    {
        [Key]
        public int computerid { get; set; }
        
        public string serialnumber { get; set; }
        public string brand { get; set; }
        public string description { get; set; }
        public bool availability { get; set; }

        [ForeignKey("media")]
        public int mediaid { get; set; }
        public media media { get; set; }
    }
}
