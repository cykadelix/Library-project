using Library_project.Data.Enums;
using Library_project.Data.Objects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_project.Models
{
    public class movies
    {
        [ForeignKey("media")]
        public int mediaid { get; set; }
        public medias? media { get; set; }

        [Key]
        public int movieid { get; set; }

        public int? rating { get; set; }
        public string? title { get; set; }
        public string? director { get; set; }
        public int genres { get; set; }
        public int length { get; set; }
        public DateOnly releasedate { get; set; }

        public bool availability { get; set;}
        public string description { get; set; }

       
        


    }
}
