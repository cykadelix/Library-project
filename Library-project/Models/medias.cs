using Library_project.Data.Objects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_project.Models
{
    public class medias
    {
        [Key]
        public int mediaid { get; set; }
        [NotMapped]
        public Location? location { get; set; }

        ICollection<reviews>? reviews { get; set; }
        

    }
}
