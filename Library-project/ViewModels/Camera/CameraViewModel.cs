using System.ComponentModel.DataAnnotations;

namespace Library_project.ViewModels.Camera
{
    public class CameraViewModel
    {
        public int cameraid { get; set; }

        [Required(ErrorMessage = "Please enter the serial number!")]
        public string? serialnumber { get; set; }
        [Required(ErrorMessage = "Please enter the brand!")]
        public string? brand { get; set; }
        public string? description { get; set; }
        [Required(ErrorMessage = "Please enter the megapixels!")]
        public double? megapixels { get; set; }
        public bool? availability { get; set; }
    }
}
