using Library_project.Data.Objects;
using System.ComponentModel.DataAnnotations;

namespace Library_project.Models
{
    public class Journal
    {
        [Key]
        public int JournalId { get; set; }
        public string Title { get; set; }
        public string Researchers { get; set; }
        public string Subject { get; set; }
        public Location Location { get; set; }
        public int Length { get; set; }
        public DateOnly DateReleased { get; set; }


    }
}
