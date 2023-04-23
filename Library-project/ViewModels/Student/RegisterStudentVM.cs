using System.ComponentModel.DataAnnotations;

namespace Library_project.ViewModels.Student
{
    public class RegisterStudentVM
    {
        [Required(ErrorMessage = "Please enter your name!")]
        public string? fname { get; set; }
        [Required(ErrorMessage = "Please enter your last name!")]
        public string? lname { get; set; }
        public string? mname{ get; set; }=String.Empty;
        [Required(ErrorMessage = "Please enter your address!")]
        public string? homeaddress { get; set; }
        [Required(ErrorMessage = "Please enter your password!")]
        public string? password { get; set; }
        [Compare("password", ErrorMessage = "Password doesn't match")]
        public string? confirmpassword { get; set; }
        public string? email { get; set; }
        [Required(ErrorMessage = "Please enter your age!")]
        [Range(1, 150, ErrorMessage = "Please enter your age (1-150)")]
        public int age{ get; set; }
        [RegularExpression("^\\d{10}$", ErrorMessage = "Must be a 10 digit number(no spaces/hyphens)")]
        public string? phonenumber { get; set; }

        public string? errorMessage;
    }
}
