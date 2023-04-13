using Library_project.Data.Objects;

namespace Library_project.ViewModels.Journal
{
    public class CreateJournalViewModel
    {
        public int journalid { get; set; }
        public string? title { get; set; }

        public string? researchers { get; set; }
        public string? subject { get; set; }
        public bool isavailable { get; set; }
        public int? length { get; set; }
        public string? releasedate { get; set; }
    }
}
