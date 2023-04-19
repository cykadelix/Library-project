using System.ComponentModel.DataAnnotations;


namespace Library_project.ViewModels.Historian
{
    public class CreateHistorianViewModel
    {
        public int historianid { get; set; }

        [Required(ErrorMessage = "Please enter their first name!")]
        public string? fname { get; set; }
        public string? mname { get; set; }
        [Required(ErrorMessage = "Please enter their last name!")]
        public string? lname { get; set; }
        [Required(ErrorMessage = "Please enter their expertise!")]
        public string? expertise { get; set; }
        [Required(ErrorMessage = "Please enter their education!")]
        public string? education { get; set; }
        [Required(ErrorMessage = "Please enter their age!")]
        [Range(1, 150, ErrorMessage ="Please enter an age 1-150")]
        public short? age { get; set; }
        public bool active { get; set; } = true;
    }
}
