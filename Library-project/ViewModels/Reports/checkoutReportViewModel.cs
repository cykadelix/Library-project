using Library_project.ViewModels.Checkout;
using Library_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_project.ViewModels.Reports
{
    public class checkoutReportViewModel
    {
        public int checkoutid { get; set; }
        public string studentfname { get; set; }
        public string studentlname { get; set; }
        public DateOnly checkoutdate { get; set; }
        public DateOnly returndate { get; set; }
        public bool returnstatus { get; set; }
        public string q_startDate { get; set; }
        public string q_endDate { get; set; }
        public string javascripttorun { get; set; }
        public string jsonObject { get; set; }
    }
}