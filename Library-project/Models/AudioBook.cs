using Library_project.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_project.Models
{
    public class AudioBook
    {
        [Key, ForeignKey("Media.id")]
        public Media Media { get; set; }
        public int AudioBookId { get; set; }


        public Genres Genre { get; set; }
        public string Title { get; set; }
        public string Narrrator { get; set; }
        public string Author { get; set; }
        public TimeOnly Length { get; set; }
        public Boolean Availability { get; set; }

    }
}
