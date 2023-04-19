namespace Library_project.ViewModels.Book
{
    public class BookListViewModel
    {
        public int NumAvailable { get; set; } = 0;
        public List<BookViewModel>? Books { get; set; } 
    }
}
