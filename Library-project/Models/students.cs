using System.ComponentModel.DataAnnotations;

namespace Library_project.Models
{
    public class students
    {
        [Key]
        public int library_card_number { get; set; }

        //Student Info
        public string? fname { get; set; }
        public string? mname { get; set; }
        public string? lname { get; set; }
        public string? homeaddress { get; set; }
        public DateOnly birthday { get; set; }

        //Login info
        public string? email { get; set; }
        public string? password { get; set; }
        public string? phonenumber { get; set; }

        //Checkout info
        public float overdue_fees { get; set; }

    }
}
