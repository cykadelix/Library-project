namespace Library_project.ViewModels.Student
{
    public class ViewStudentVM
    {
        public string library_card_number { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string? mname { get; set; } = String.Empty;
        public string homeaddress { get; set; }
        public string birthday { get; set; }

        public string email { get; set; }

        public string phonenumber { get; set; }
        public int ?historianid { get; set; }
        public float overdueFees{ get; set; }

    }
}
