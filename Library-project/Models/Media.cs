using Library_project.Data.Objects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_project.Models
{
    public class Media
    {
        [Key]
        public int mediaId { get; set; }
        [NotMapped]
        public Location location { get; set; }

    }
}
