using System.ComponentModel.DataAnnotations;

namespace Library_project.ViewModels.Computer
{
    public class ComputerViewModel
    {
        public int computerid { get; set; }

        [Required(ErrorMessage = "Please enter the serial number!")]
        public string? serialnumber { get; set; }
        [Required(ErrorMessage = "Please enter the brand!")]
        public string? brand { get; set; }
        public string? description { get; set; }
        public bool? availability { get; set; }
        [Required(ErrorMessage = "Please enter the image!")]
        public IFormFile? image { get; set; }
        public byte[]? imageBytes { get; set; }
    }
}
