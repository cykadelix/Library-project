using System;
using System.Collections.Generic;
using System.Globalization;
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

        public IActionResult CheckoutsByStudentIndex()
        {
            return View();
        }

        public List<checkoutsByStudentReportViewModel>? CheckoutsByStudentToList(int lcn)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));
            using var dataSource = dataSourceBuilder.Build();
            string comm = "SELECT library_card_number, fname, lname, null as brand, null as serialnumber, title, checkoutdate, returndate, returned, returned_date, 'Book' as mediatype, checkouts.mediaid " +
                "FROM students, checkouts, books " +
                "WHERE ( checkouts.studentid = '" + lcn + "' AND checkouts.mediaid = books.mediaid AND students.library_card_number = '" + lcn + "') UNION " +
                "SELECT library_card_number, fname, lname, brand, serialnumber, null as title, checkoutdate, returndate, returned, returned_date, 'Camera' as mediatype, checkouts.mediaid " +
                "FROM students, checkouts, cameras " +
                "WHERE ( checkouts.studentid = '" + lcn + "' AND checkouts.mediaid = cameras.mediaid AND students.library_card_number = '" + lcn + "') UNION " +

                "SELECT library_card_number, fname, lname, brand, serialnumber, null as title, checkoutdate, returndate, returned, returned_date, 'Computer' as mediatype, checkouts.mediaid " +
                "FROM students, checkouts, computers " +
                "WHERE ( checkouts.studentid = '" + lcn + "' AND checkouts.mediaid = computers.mediaid AND students.library_card_number = '" + lcn + "') UNION " +

                "SELECT library_card_number, fname, lname, null as brand, null as serialnumber, title, checkoutdate, returndate, returned, returned_date, 'Audiobook' as mediatype, checkouts.mediaid " +
                "FROM students, checkouts, audiobooks " +
                "WHERE ( checkouts.studentid = '" + lcn + "' AND checkouts.mediaid = audiobooks.mediaid AND students.library_card_number = '" + lcn + "') UNION " +

                "SELECT library_card_number, fname, lname, null as brand, null as serialnumber, title, checkoutdate, returndate, returned, returned_date, 'Journal' as mediatype, checkouts.mediaid " +
                "FROM students, checkouts, journals " +
                "WHERE ( checkouts.studentid = '" + lcn + "' AND checkouts.mediaid = journals.mediaid AND students.library_card_number = '" + lcn + "') UNION " +

                "SELECT library_card_number, fname, lname, null as brand, null as serialnumber, title, checkoutdate, returndate, returned, returned_date, 'Movie' as mediatype, checkouts.mediaid " +
                "FROM students, checkouts, movies " +
                "WHERE ( checkouts.studentid = '" + lcn + "' AND checkouts.mediaid = movies.mediaid AND students.library_card_number = '" + lcn + "') UNION " +

                "SELECT library_card_number, fname, lname, brand, serialnumber, null as title, checkoutdate, returndate, returned, returned_date, 'Projector' as mediatype, checkouts.mediaid " +
                "FROM students, checkouts, projectors " +
                "WHERE ( checkouts.studentid = '" + lcn + "' AND checkouts.mediaid = projectors.mediaid AND students.library_card_number = '" + lcn + "')";

            using var command = dataSource.CreateCommand(comm);
            using var reader = command.ExecuteReader();

            var LocalList = new List<checkoutsByStudentReportViewModel>();
            while (reader.Read())
            {
                LocalList.Add(new checkoutsByStudentReportViewModel()
                {
                    library_card_number = reader.GetInt32(0),
                    fname = reader.GetString(1),
                    lname = reader.GetString(2),
                    Brand = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    SerialNo = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    Title = reader.IsDBNull(5) ? "" : reader.GetString(5),
                    CheckoutDate = reader.GetDateTime(6).ToString("MM-dd-yyyy HH:mm"),
                    ReturnDate = reader.GetDateTime(7).ToString("MM-dd-yyyy HH:mm"),
                    Returned = reader.IsDBNull(8) ? false : reader.GetBoolean(8),
                    ReturnedDate = reader.IsDBNull(9) ? "" : reader.GetDateTime(9).ToString(),
                    MediaType = reader.GetString(10),
                    mediaId = reader.GetInt32(11),
                });
            }
            if (LocalList.Count == 0)
            {
                return null;
            }
            return LocalList;
        }
        [HttpGet]
        public IActionResult GetCheckoutsByStudentList(int library_card_number)
        {
            return Json(CheckoutsByStudentToList(library_card_number));
        }

        [IgnoreAntiforgeryToken]
        public List<checkoutReportViewModel>? CheckoutsByDateToList(checkoutReportViewModel covm)
        {
            if (covm.q_startDate == null || covm.q_endDate == null)
            {
                return null;
            }
            covm.q_startDate = DateTime.Parse(covm.q_startDate, CultureInfo.InvariantCulture).ToString();
            covm.q_endDate = DateTime.Parse(covm.q_endDate, CultureInfo.InvariantCulture).ToString();
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));
            using var dataSource = dataSourceBuilder.Build();
            using var command = dataSource.CreateCommand("SELECT checkouts.checkoutid, students.fname, students.lname, checkouts.checkoutdate, checkouts.returndate, checkouts.returned " +
                                                         "FROM checkouts, students " +
                                                         "WHERE checkouts.checkoutdate >= '" + covm.q_startDate + "' AND checkouts.checkoutdate <= '" + covm.q_endDate + "' AND checkouts.studentid = students.library_card_number " +
                                                         "UNION SELECT checkouts.checkoutid, employees.fname, employees.lname, checkouts.checkoutdate, checkouts.returndate, checkouts.returned " +
                                                         "FROM checkouts, employees " +
                                                         "WHERE checkouts.checkoutdate >= '" + covm.q_startDate + "' AND checkouts.checkoutdate <= '" + covm.q_endDate + "' AND checkouts.employeeid = employees.employeeid");
            using var reader = command.ExecuteReader();

            var LocalList = new List<checkoutReportViewModel>();
            while (reader.Read())
            {
                LocalList.Add(new checkoutReportViewModel()
                {
                    checkoutid = reader.GetInt32(0),
                    studentfname = (string)reader.GetValue(1),
                    studentlname = (string)reader.GetValue(2),
                    checkoutdate = reader.GetDateTime(3).ToString("MM-dd-yyyy HH:mm"),
                    returndate = reader.GetDateTime(4).ToString("MM-dd-yyyy HH:mm"),
                    returnstatus = reader.GetBoolean(5),
                });
            }
            if (LocalList.Count == 0)
            {
                return null;
            }
            return LocalList;

        }

        [HttpGet]
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
            if (rrvm.mediaid == null) return null;
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));
            using var dataSource = dataSourceBuilder.Build();
            using var command = dataSource.CreateCommand("SELECT students.fname, students.lname, reviews.mediaid, reviews.rating, reviews.description " +
                                                         "FROM students, reviews " +
                                                         "WHERE reviews.mediaid = '" + rrvm.mediaid + "' AND students.library_card_number = reviews.studentid");
            using var reader = command.ExecuteReader();

            var LocalList = new List<reviewsReportViewModel>();

            double averageRating = 0;
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
                averageRating += reader.GetInt32(3);
            }
            if (LocalList.Count == 0)
            {
                return null;
            }
            LocalList.Add(new reviewsReportViewModel()
            {
                averageRating = averageRating / LocalList.Count(),
            });
            return LocalList;

        }

        [HttpGet]
        public IActionResult GetReviewsList(reviewsReportViewModel rrvm)
        {
            return Json(ReviewsToList(rrvm));
        }
        //------------------ End of Ratings Report ----------------------->>

    }
}