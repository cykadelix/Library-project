using Library_project.Data.Enums;
using Library_project.Data.Objects;
using Library_project.Models;
using Library_project.ViewModels.Reports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Npgsql;

namespace Library_project.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IConfiguration _config;
        public ReportsController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult CheckoutsByDateIndex()
        {
            return View();
        }

        [IgnoreAntiforgeryToken]
        public List<checkoutReportViewModel>? CheckoutsByDateToList(checkoutReportViewModel covm)
        {

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));
            using var dataSource = dataSourceBuilder.Build();
            using var command = dataSource.CreateCommand("SELECT checkouts.checkoutid, students.fname, students.lname, checkouts.checkoutdate, checkouts.returndate, checkouts.returned " +
                                                         "FROM checkouts, students " +
                                                         "WHERE checkouts.checkoutdate >= '" + covm.q_startDate + "' AND checkouts.checkoutdate <= '" + covm.q_endDate + "' AND checkouts.studentid = students.library_card_number");
            using var reader = command.ExecuteReader();

            var LocalList = new List<checkoutReportViewModel>();
            while (reader.Read())
            {
                LocalList.Add(new checkoutReportViewModel()
                {
                    checkoutid = (int)reader.GetInt32(0),
                    studentfname = (string)reader.GetValue(1),
                    studentlname = (string)reader.GetValue(2),
                    checkoutdate = (DateTime)reader.GetDateTime(3),
                    returndate = (DateTime)reader.GetDateTime(4),
                    returnstatus = (bool)reader.GetBoolean(5),
                });
            }
            if (LocalList.Count == 0)
            {
                return null;
            }
            return LocalList;

        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult GetCheckoutsByDateList(checkoutReportViewModel covm)
        {
            return Json(CheckoutsByDateToList(covm));
        }
        //------------------ End of Checkouts Report ----------------------->>

        //------------------ Start of Ratings Report ----------------------->>
        public IActionResult AverageRatingIndex()
        {
            return View();
        }

        public List<reviewsReportViewModel>? ReviewsToList(reviewsReportViewModel rrvm)
        {

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));
            using var dataSource = dataSourceBuilder.Build();
            using var command = dataSource.CreateCommand("SELECT students.fname, students.lname, reviews.mediaid, reviews.rating, reviews.evaluation " +
                                                         "FROM students, reviews " +
                                                         "WHERE reviews.mediaid = '" + rrvm.mediaid + "' AND students.library_card_number = reviews.studentid");
            using var reader = command.ExecuteReader();

            var LocalList = new List<reviewsReportViewModel>();
            
            while (reader.Read())
            {
                LocalList.Add(new reviewsReportViewModel()
                {
                    fname = (string)reader.GetValue(0),
                    lname = (string)reader.GetValue(1),
                    mediaid = (int)reader.GetInt32(2),
                    reviewRating = (int)reader.GetInt32(3),
                    evaluation = (string)reader.GetValue(4),
                });
            }
            if (LocalList.Count == 0)
            {
                return null;
            }
            return LocalList;

        }

        [HttpPost]
        public IActionResult GetReviewsList(reviewsReportViewModel rrvm)
        {
            return Json(ReviewsToList(rrvm));
        }
        //------------------ End of Ratings Report ----------------------->>

    }
}
