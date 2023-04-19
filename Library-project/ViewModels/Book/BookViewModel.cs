using System.ComponentModel.DataAnnotations;

namespace Library_project.ViewModels.Book
{
    public class BookViewModel
    {
        public int bookid { get; set; }

        [Required(ErrorMessage = "Please enter the title!")]
        public string? title { get; set; }
        [Required(ErrorMessage = "Please enter the author(s)!")]
        public string? author { get; set; }
        [Required(ErrorMessage = "Please enter the genre!")]
        public int? genres { get; set; }
        [Required(ErrorMessage = "Please enter the publishing date!")]
        public string? publicDate { get; set; }
        [Required(ErrorMessage = "Please enter the page count!")]
        public int? pageCount { get; set; }
        [Required(ErrorMessage = "Please enter the isbn!")]
        public long? isbn { get; set; }
        public bool? isAvailable { get; set; }
        public string? description { get; set; }
        [Required(ErrorMessage = "Please enter an image!")]
        public IFormFile? image { get; set; }
    }
}
