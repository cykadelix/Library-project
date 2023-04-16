using Library_project.Models;

namespace Library_project.ViewModels.Historian
{
    public class EditHistorianViewModel
    {

        public string? fname { get; set; } = string.Empty;
        public string? mname { get; set; } = string.Empty;
        public string? lname { get; set; } = string.Empty;
       // public int? historianID { get; set; }
        public string? expertise { get; set; } = string.Empty;
        public string? education { get; set; } = string.Empty;
        public short? age { get; set; }

        //Relationships
        //public List<students>? studentstosee { get; set; }

    }
}