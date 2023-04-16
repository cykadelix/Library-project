using Library_project.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_project.Models
{
    public class audiobooks
    {
        [ForeignKey("media")]
        public int mediaid { get; set; }
        public medias? media { get; set; }

        [Key]
        public int audiobookid { get; set; }

        public genres genre { get; set; }
        public string? title { get; set; }
        public string? narrator { get; set; }
        public string? author { get; set; }
        public TimeOnly length { get; set; }
        public Boolean availability { get; set; }

        

    }
}
