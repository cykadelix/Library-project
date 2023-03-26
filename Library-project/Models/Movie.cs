using Library_project.Data.Enums;
using Library_project.Data.Objects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_project.Models
{
    public class movie
    {
        [ForeignKey("media")]
        public int mediaid { get; set; }
        public media media { get; set; }

        [Key]
        public int movieId { get; set; }

        public int rating { get; set; }
        public string title { get; set; }
        public string director { get; set; }
        public genres genres { get; set; }
        public TimeOnly length { get; set; }
        public DateOnly releasedate { get; set; }

        public Boolean availability { get; set;}

       
        


    }
}
