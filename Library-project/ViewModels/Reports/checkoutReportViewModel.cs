using Library_project.ViewModels.Checkout;
using Library_project.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Library_project.ViewModels.Reports
{
    public class checkoutReportViewModel
    {
        public int? checkoutid { get; set; }
        public string? studentfname { get; set; }
        public string? studentlname { get; set; }
        public string? checkoutdate { get; set; }
        public string? returndate { get; set; }
        public bool? returnstatus { get; set; }
        public string? q_startDate { get; set; }
        public string? q_endDate { get; set; }
    }
}