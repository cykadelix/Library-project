using Library_project.ViewModels.Checkout;
using Library_project.Models;
using System;

namespace Library_project.ViewModels.Reports
{
    public class checkoutReportViewModel
    {
        public int checkoutid { get; set; }
        public string studentfname { get; set; }
        public string studentlname { get; set; }
        public DateTime checkoutdate { get; set; }
        public DateTime returndate { get; set; }
        public bool returnstatus { get; set; }
        public string q_startDate { get; set; }
        public string q_endDate { get; set; }
    }
}