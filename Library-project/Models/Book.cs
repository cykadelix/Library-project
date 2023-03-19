using Library_project.Data.Enums;
using Library_project.Data.Objects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_project.Models
{
    public class Book
    {
        [Key, ForeignKey("Media")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string[] Author { get; set; }
        public Genres[] Genres { get; set; }
        public DateOnly PublicDate { get; set; }

        public Location location { get; set; }
        public int PageCount { get; set; }

        public int ISBN { get; set; }
        public Boolean isAvailable { get; set; }


    }
}
