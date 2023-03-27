using Library_project.Data.Objects;

namespace Library_project.ViewModels.Journal
{
    public class CreateJournalViewModel
    {
        
        
        public string title { get; set; }

        public Location? location { get; set; }
        public string[] researchers { get; set; }
        public string[] subject { get; set; }
        public bool isavailable { get; set; }
        public int length { get; set; }
        public DateOnly releasedate { get; set; }
    }
}
