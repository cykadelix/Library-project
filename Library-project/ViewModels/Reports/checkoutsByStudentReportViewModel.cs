using System;
namespace Library_project.ViewModels.Reports
{
	public class checkoutsByStudentReportViewModel
	{
        public int library_card_number { get; set; }
        public int checkoutId { get; set; }
        public int mediaId { get; set; }
        public string? fname { get; set; }
        public string? lname { get; set; }
        public string? MediaType { get; set; }
        public string? Brand { get; set; }
        public string? SerialNo { get; set; }
        public string? Title { get; set; }
        public string? CheckoutDate { get; set; }
        public string? ReturnDate { get; set; }
        public bool? Returned { get; set; }
        public string? ReturnedDate { get; set; }
    }
}

