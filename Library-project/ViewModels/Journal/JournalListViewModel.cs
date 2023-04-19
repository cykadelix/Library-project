namespace Library_project.ViewModels.Journal
{
    public class JournalListViewModel
    {
        public int NumAvailable { get; set; } = 0;
        public List<JournalViewModel>? Journals { get; set; }
    }
}
