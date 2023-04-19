namespace Library_project.ViewModels.Student
{
    public class RegisterStudentVM
    {
        public string fname { get; set; }
        public string lname { get; set; }
        public string mname{ get; set; }=String.Empty;
        public string homeaddress { get; set; }
        
        public string password { get; set; }
        public string email { get; set; }
        
        public int age{ get; set; }
        public string phonenumber { get; set; }


    }
}
