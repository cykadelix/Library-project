using Library_project.Data.Enums;

namespace Library_project.ViewModels.Book
{
    public class exampleBookModel
    {
        public string title { get; set; }
        public string author { get; set; }
        public int pageCount { get; set; }
        public int isbn { get; set; }
        public DateOnly publishDate { get; set; }
        public int genre { get; set; }
        public bool isAvailable { get; set; }
    }
}
