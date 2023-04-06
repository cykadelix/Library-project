using Library_project.Models;

namespace Library_project.ViewModels
{
    public class CreateHistorianViewModel
    {
        public string fName { get; set; } = string.Empty;
        public string mName { get; set; } = string.Empty;
        public string lName { get; set; } = string.Empty;
       // public int historianID { get; set; }
        public string expertise { get; set; } = string.Empty;
        public string education { get; set; } = string.Empty;
        public short age { get; set; }

        //Relationships
        public List<student> studentsToSee { get; set; }
    }

}
