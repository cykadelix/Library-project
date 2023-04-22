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
        public List<checkoutUserReportViewModel>? CheckoutsByUserToList(checkoutUserReportViewModel model)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));
            using var dataSource = dataSourceBuilder.Build();
            using var command = dataSource.CreateCommand("SELECT students.library_card_number, students.fname, students.lname" + "FROM students, medias, checkouts " + "WHERE students.library_card_number=checkouts.studentid" + "AND checkouts.mediaid=medias.mediaid");
            using var reader = command.ExecuteReader();

            var LocalList = new List<checkoutUserReportViewModel>();
            while (reader.Read())
            {
                LocalList.Add(new checkoutUserReportViewModel()
                {
                    checkoutid = (int)reader.GetInt32(0),
                    fname = (string)reader.GetValue(1),
                    lname = (string)reader.GetValue(2),
                    lcn = (int)reader.GetInt32(3),
                    mediaid = (int)reader.GetInt32(4)
                });
            }
            if (LocalList.Count == 0)
            {
                return null;
            }
            return LocalList;
        }
        [HttpPost]
        public IActionResult GetCheckoutsByUserList(checkoutUserReportViewModel urvm)
        {
            return Json(CheckoutsByUserToList(urvm));
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

        [HttpGet]
        public IActionResult GetReviewsList(reviewsReportViewModel rrvm)
        {
            return Json(ReviewsToList(rrvm));
        }
        //------------------ End of Ratings Report ----------------------->>

    }
}