using Library_project.Data.Enums;
using Library_project.Data.Objects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_project.Models
{
    public class Movie
    {
        [Key, ForeignKey("Media.id")]
        public Media Media { get; set; }
        public int MovieId { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public Genres Genres { get; set; }
        public TimeOnly Length { get; set; }
        public DateOnly ReleasDate { get; set; }

        public Boolean Availability { get; set;}


    }
}
