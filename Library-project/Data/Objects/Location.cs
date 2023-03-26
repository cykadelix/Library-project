using System.ComponentModel.DataAnnotations.Schema;

namespace Library_project.Data.Objects
{
    public class Location
    {
        [NotMapped]
        public int f { get; set; }
        public int r { get; set; }
    }
}
