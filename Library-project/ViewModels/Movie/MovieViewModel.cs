using System.ComponentModel.DataAnnotations;

namespace Library_project.ViewModels.Movie
{
    public class MovieViewModel
    {
        public int movieid { get; set; }
        [Required(ErrorMessage = "Please enter the rating(1-10)!")]
        [Range(1, 10, ErrorMessage = "Enter a whole number between 1 and 10")]
        public int? rating { get; set; }
        [Required(ErrorMessage = "Please enter the title!")]
        public string? title { get; set; }
        [Required(ErrorMessage = "Please enter the director!")]
        public string? director { get; set; }
        [Required(ErrorMessage = "Please enter the genre!")]
        public int? genres { get; set; }
        [Required(ErrorMessage = "Please enter the length!")]
        public int? length { get; set; }
        [Required(ErrorMessage = "Please enter the date!")]
        public string? releasedate { get; set; }

        public bool? availability { get; set; }
    }
}
