using System.ComponentModel.DataAnnotations;

namespace Library_project.ViewModels.Student
{
    public class EditStudentVM
    {
       
        public int library_card_number { get; set; }

        //Student Info
        [Required(ErrorMessage = "Please enter their first name!")]
        public string? fname { get; set; }
        public string? mname { get; set; }
        [Required(ErrorMessage = "Please enter their last name!")]
        public string? lname { get; set; }

        [Required(ErrorMessage = "Please enter their address!")]
        public string? homeaddress { get; set; }

        [Required(ErrorMessage = "Please enter their age!")]
        public int age { get; set; }

        //Login info
        [Required(ErrorMessage = "Please enter their email!")]
        public string? email { get; set; }
        [Required(ErrorMessage = "Please enter their password!")]
        public string? password { get; set; }
        [Compare("password", ErrorMessage = "Password doesn't match")]
        public string? confirmpassword { get; set; }

        [RegularExpression("^\\d{10}$", ErrorMessage = "Must be a 10 digit number(no spaces/hyphens)")]
        public string? phonenumber { get; set; }

        //Checkout info
        public float overdue_fees { get; set; }
    }
}
