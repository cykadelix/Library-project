using Library_project.Data.Objects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_project.Models
{
    public class media
    {
        [Key]
        public int mediaid { get; set; }
        [NotMapped]
        public Location location { get; set; }

        ICollection<review>? reviews { get; set; }
        

    }
}
