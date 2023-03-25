using Library_project.Data.Objects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_project.Models
{
    public class journal
    {
        
        [ForeignKey("media")]
        public int mediaId { get; set; }
        public media media { get; set; }
        

        [Key]
        public int jouranalId { get; set; }
        public string title { get; set; }
        public string researchers { get; set; }
        public string subject { get; set; }
        
        public int length { get; set; }
        public DateOnly dateReleased { get; set; }


    }
}
