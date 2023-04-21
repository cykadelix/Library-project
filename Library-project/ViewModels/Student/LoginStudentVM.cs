using System.ComponentModel.DataAnnotations;

namespace Library_project.ViewModels.Student

{
    public class LoginStudentVM
    {
        [Required(ErrorMessage = "Please enter your email!")]
        public string? email { get; set; }
        [Required(ErrorMessage = "Please enter your password!")]
        public string? password { get; set; }
        public string errorMessage { get; set; }
    }
}
