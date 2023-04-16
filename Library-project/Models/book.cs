using Library_project.Data.Enums;
using Library_project.Data.Objects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_project.Models
{
    public class books
    {
        [Key]
        public int bookid { get; set; }

        
        public string title { get; set; }
        public string author { get; set; }
        public int genres { get; set; }
        public DateOnly publicdate { get; set; }

        [NotMapped]
        public Location? location { get; set; }
        public int pagecount { get; set; }

        public long isbn { get; set; }
        public Boolean isavailable { get; set; }
        
        [ForeignKey("media")]
        public int mediaid { get; set; }
        public medias media { get; set; }

       

    }
}
