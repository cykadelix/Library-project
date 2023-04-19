namespace Library_project.ViewModels.Movie
{
    public class MovieListViewModel
    {
        public int NumAvailable { get; set; } = 0;
        public List<MovieViewModel>? Movies { get; set; }
    }
}
