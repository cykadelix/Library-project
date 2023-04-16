using System.ComponentModel.DataAnnotations;

namespace Library_project.ViewModels.Journal
{
    public class CreateJournalViewModel
    {
        public int journalid { get; set; }

        [Required(ErrorMessage = "Please enter the title!")]
        public string? title { get; set; }
        [Required(ErrorMessage = "Please enter the researcher(s)!")]
        public string? researchers { get; set; }
        [Required(ErrorMessage = "Please enter the subject(s)!")]
        public string? subject { get; set; }
        [Required(ErrorMessage = "Please enter the length!")]
        public int? length { get; set; }
        [Required(ErrorMessage = "Please enter the date!")]
        public string? releasedate { get; set; }
        public bool? isavailable { get; set; }

    }
}
