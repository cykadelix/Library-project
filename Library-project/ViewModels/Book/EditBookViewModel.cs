using Library_project.Data.Enums;

namespace Library_project.ViewModels.Book
{
    public class EditBookViewModel
    {
        /*
        [Key]
        public int bookId { get; set; }
        */

        public string? title { get; set; }
        public string[]? author { get; set; }
        public genres[]? genres { get; set; }
        public DateOnly? publicDate { get; set; }


        public int? pageCount { get; set; }

        public int? isbn { get; set; }
        public bool? isAvailable { get; set; }


        /*
        [ForeignKey("media")]
        public int mediaId { get; set; }
        public media media { get; set; }
        */
    }
}
