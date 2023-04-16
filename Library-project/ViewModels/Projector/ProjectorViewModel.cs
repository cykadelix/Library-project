using System.ComponentModel.DataAnnotations;

namespace Library_project.ViewModels.Projector
{
    public class ProjectorViewModel
    {
        public int projectorid { get; set; }
        [Required(ErrorMessage = "Please enter the serial number!")]
        public string? serialnumber { get; set; }
        [Required(ErrorMessage = "Please enter the brand!")]
        public string? brand { get; set; }
        public string? description { get; set; }
        [Required(ErrorMessage = "Please enter the lumens!")]
        public int? lumens { get; set; }
        public bool? availability { get; set; }
    }
}
