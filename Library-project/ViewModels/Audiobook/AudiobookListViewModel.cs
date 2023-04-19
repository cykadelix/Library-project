namespace Library_project.ViewModels.Audiobook
{
    public class AudiobookListViewModel
    {
        public int NumAvailable { get; set; } = 0;
        public List<AudiobookViewModel>? Audiobooks { get; set; }
    }
}
