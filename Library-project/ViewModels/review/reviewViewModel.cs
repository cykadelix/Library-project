using System.ComponentModel.DataAnnotations;

namespace Library_project.ViewModels.Review
{
    public class ReviewViewModel
    {
        [Required(ErrorMessage = "Please enter the rating(1-10)!")]
        [Range(1, 10, ErrorMessage = "Enter a number between 1 and 10")]
        public int? rating { get; set; }
        [Required(ErrorMessage = "Please enter an evaluation!")]
        public string? description { get; set; }
        [Required(ErrorMessage = "Please enter a media ID")]
        public int? mediaid { get; set; }
        public int? studentid { get; set; }
        public int? employeeid { get; set; }
        public string? resultmessage { get; set; }
    }
}
