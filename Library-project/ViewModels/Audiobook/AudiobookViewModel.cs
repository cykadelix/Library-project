using System.ComponentModel.DataAnnotations;

namespace Library_project.ViewModels.Audiobook
{
    public class AudiobookViewModel
    {
        public int? audiobookid { get; set; }
        [Required(ErrorMessage = "Please enter the genre!")]
        public int? genre { get; set; }
        [Required(ErrorMessage = "Please enter the title!")]
        public string? title { get; set; }
        [Required(ErrorMessage = "Please enter the narrator!")]

        public string? narrator { get; set; }
        [Required(ErrorMessage = "Please enter the author!")]

        public string? author { get; set; }
        [Required(ErrorMessage = "Please enter the length!")]
        [RegularExpression("^(?:[0-9]|[0-1][0-9]|2[0-3]):(?:[0-9]|[0-5][0-9]):(?:[0-9]|[0-5][0-9])$", ErrorMessage="Must be formatted in (hh:mm:ss)!")]
        public string? length { get; set; }
        public bool? availability { get; set; }
        public string? description { get; set; }
        [Required(ErrorMessage = "Please enter an image!")]
        public IFormFile? image { get; set; }
        public byte[]? imageBytes { get; set; }
    }
}
