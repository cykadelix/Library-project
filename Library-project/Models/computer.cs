using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_project.Models
{
    public class computer
    {
        [Key]
        public int SerialNumber { get; set; }

        public string Brand { get; set; }
        public string Description { get; set; }
        public bool Availibility { get; set; }

        [ForeignKey("media")]
        public int mediaId { get; set; }
        public media media { get; set; }


        

        public computer()
        {

        }
    }
}
