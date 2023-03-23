using Library_project.Data.Objects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_project.Models
{
    public class Journal
    {
        [Key, ForeignKey("Media.id")]
        public Media Media { get; set; }
        public int JournalId { get; set; }
        public string Title { get; set; }
        public string Researchers { get; set; }
        public string Subject { get; set; }
        
        public int Length { get; set; }
        public DateOnly DateReleased { get; set; }


    }
}
