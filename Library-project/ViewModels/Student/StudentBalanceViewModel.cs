using System;
using System.ComponentModel.DataAnnotations;
namespace Library_project.ViewModels.Student
{
    public class StudentBalanceViewModel
    {
        [Required(ErrorMessage = "Please enter an amount to pay!")]
        public int amount { get; set; }
    }
}
