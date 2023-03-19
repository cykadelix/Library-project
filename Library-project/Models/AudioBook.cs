using Library_project.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Library_project.Models
{
    public class AudioBook
    {
        [Key]
        public int ID { get; set; }
        public Genres Genre { get; set; }
        public string Title { get; set; }
        public string Narrrator { get; set; }
        public string Author { get; set; }
        public TimeOnly Length { get; set; }
        public Boolean Availability { get; set; }

    }
}
