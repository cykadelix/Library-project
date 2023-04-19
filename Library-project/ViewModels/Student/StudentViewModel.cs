using System.ComponentModel.DataAnnotations;

namespace Library_project.ViewModels.Student
{
    public class StudentViewModel
    {
        [Required(ErrorMessage = "Please enter a username!")]
        public string? username { get; set; }
        [Required(ErrorMessage = "Please enter a password!")]
        public string? password { get; set; }
    }
}
