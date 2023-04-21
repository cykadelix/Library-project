using Library_project.ViewModels.Review;
using Library_project.Models;

namespace Library_project.ViewModels.Reports
{
    public class reviewsReportViewModel
    {
        public string? fname { get; set; }
        public string? lname { get; set; }
        public int? mediaid { get; set; }
        public int? reviewRating { get; set; }
        public string? evaluation { get; set; }
        public double? averageRating { get; set; }
    }
}