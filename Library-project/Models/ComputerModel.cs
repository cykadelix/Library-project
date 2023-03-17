using System.ComponentModel.DataAnnotations;

namespace Library_project.Models
{
    public class ComputerModel
    {
        [Key]
        public int serialNumber { get; set; }

        public string brand { get; set; }
        public string description { get; set; }
        public bool Availibility { get; set; }

        public ComputerModel()
        {

        }
    }
}
