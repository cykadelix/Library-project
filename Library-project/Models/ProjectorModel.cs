using System.ComponentModel.DataAnnotations;

namespace Library_project.Models
{
    public class ProjectorModel
    {
        [Key]
        public int SerialNumber { get; set; }

        public string Brand { get; set; }
        public string Description { get; set; }
        public int Lumens { get; set; }
        public bool Availibility { get; set; }

        public ProjectorModel()
        {

        }
    }
}
