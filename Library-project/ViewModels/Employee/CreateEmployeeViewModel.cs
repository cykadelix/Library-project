using Library_project.Models;

namespace Library_project.ViewModels
{
    public class CreateEmployeeViewModel
    {
        public string fName { get; set; } = string.Empty;
        public string mName { get; set; } = string.Empty;
        public string lName { get; set; } = string.Empty;
       // public int employeeID { get; set; }
       // public int supervisorID { get; set; }
        public string position { get; set; } = string.Empty;
        public float salary { get; set; }
        public short age { get; set; }
        public string eMail { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string homeAddress { get; set; } = string.Empty;
        public string phoneNumber { get; set; } = string.Empty;
        public Employee supervisor { get; set; }
    }

}
