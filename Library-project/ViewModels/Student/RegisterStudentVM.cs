namespace Library_project.ViewModels.Student
{
    public class RegisterStudentVM
    {
        public string fname { get; set; }
        public string lname { get; set; }
        public string mname{ get; set; }=String.Empty;
        public string homeaddress { get; set; }
        public DateOnly birthday { get; set; }

        public string email { get; set; }
        
        public string phonenumber { get; set; }


    }
}
