using Library_project.Models;

namespace Library_project.ViewModels.Employee
{
    public class EditEmployeeViewModel
    {
        public string? fname { get; set; } = string.Empty;
        public string? mname { get; set; } = string.Empty;
        public string? lname { get; set; } = string.Empty;
       // public int? employeeID { get; set; }
       // public int? supervisorID { get; set; }
        public string? position { get; set; } = string.Empty;
        public float? salary { get; set; }
        public short? age { get; set; }
        public string? email { get; set; } = string.Empty;
        public string? password { get; set; } = string.Empty;
        public string? homeaddress { get; set; } = string.Empty;
        public string? phonenumber { get; set; } = string.Empty;
        public employees? supervisor { get; set; }
    }

}
