using Library_project.Data.Enums;
using Library_project.Data.Objects;
using System.ComponentModel.DataAnnotations;

namespace Library_project.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public Location Location { get; set; }
        public Genres Genres { get; set; }
        public TimeOnly Length { get; set; }
        public DateOnly ReleasDate { get; set; }

        public Boolean Availability { get; set;}


    }
}
