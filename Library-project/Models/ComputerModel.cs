using System.ComponentModel.DataAnnotations;

namespace Library_project.Models
{
    public class ComputerModel
    {
        [Key]
        public int SerialNumber { get; set; }

        public string Brand { get; set; }
        public string Description { get; set; }
        public bool Availibility { get; set; }

        public ComputerModel()
        {

        }
    }
}
