namespace Library_project.ViewModels.Student
{
    public class ViewStudentVM
    {
        public int library_card_number { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string? mname { get; set; } = String.Empty;
        public string homeaddress { get; set; }
        

        public string email { get; set; }
        public string password { get; set; }

        public int age { get; set; }
        public string phonenumber { get; set; }
       
        public float overdueFees{ get; set; }

    }
}
