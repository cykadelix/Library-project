using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_project.ViewModels.Employee
{
    public class CreateEmployeeViewModel
    {
        public int employeeid { get; set; }
        [Required(ErrorMessage = "Please enter their first name!")]
        public string? fname { get; set; }
        public string? mname { get; set; }
        [Required(ErrorMessage = "Please enter their last name!")]
        public string? lname { get; set; }
        [Required(ErrorMessage = "Please enter their position!")]
        public string? position { get; set; }
        [Required(ErrorMessage = "Please enter their salary!")]
        public float? salary { get; set; }
        [Required(ErrorMessage = "Please enter their age!")]
        public short? age { get; set; }
        [Required(ErrorMessage = "Please enter their email!")]
        public string? email { get; set; }
        [Required(ErrorMessage = "Please enter their password!")]
        public string? password { get; set; }
        [Compare("password", ErrorMessage = "Password doesn't match")]
        public string? confirmpassword { get; set; }
        [Required(ErrorMessage = "Please enter their address!")]
        public string? homeaddress { get; set; }
        [Required(ErrorMessage = "Please enter their phone number!")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Must be a 10 digit number(no spaces/hyphens)")]
        public string? phonenumber { get; set; }
    }

}
