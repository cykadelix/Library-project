using System;
using System.ComponentModel.DataAnnotations;
namespace Library_project.ViewModels.Student
{
    public class CreateStudentViewModel
    {
        public int library_card_number { get; set; }

        [Required(ErrorMessage = "Please enter their first name!")]
        public string? fname { get; set; }
        public string? mname { get; set; }
        [Required(ErrorMessage = "Please enter their last name!")]
        public string? lname { get; set; }
        public string? homeaddress { get; set; }
        [Required(ErrorMessage = "Please enter their email!")]
        public string? email { get; set; }
        [Required(ErrorMessage = "Please enter their password!")]
        public string? password { get; set; }
        public string? phonenumber { get; set; }
        public int age { get; set; }
        public int overdue_fees { get; set; }
    }
}
