using Library_project.Data.Enums;
using Library_project.Data.Objects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_project.Models
{
    public class book
    {
        [Key]
        public int bookId { get; set; }

        
        public string title { get; set; }
        public string[] author { get; set; }
        public int genres { get; set; }
        public DateOnly publicDate { get; set; }

        [NotMapped]
        public Location? location { get; set; }
        public int pageCount { get; set; }

        public long isbn { get; set; }
        public Boolean isAvailable { get; set; }
        
        [ForeignKey("media")]
        public int mediaId { get; set; }
        public media media { get; set; }

       

    }
}
