using System.ComponentModel.DataAnnotations;

namespace Library_project.Models
{
    public class ProjectorModel
    {
        [Key]
        public int serialNumber { get; set; }

        public string brand { get; set; }
        public string description { get; set; }
        public int lumens { get; set; }
        public bool Availibility { get; set; }

        public ProjectorModel()
        {

        }
    }
}
