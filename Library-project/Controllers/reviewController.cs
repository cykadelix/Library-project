﻿using Library_project.ViewModels.Review;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Npgsql;

namespace Library_project.Controllers
{
    public class reviewController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public reviewController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult createReview()
        {
            var newReview = new ReviewViewModel();
            return View(newReview);
        }


        public async Task<IActionResult> CreateReviewLandingPage(ReviewViewModel newReview)
        {
            if (ModelState.IsValid)
            {
                await using NpgsqlConnection conn = new NpgsqlConnection("Host = 127.0.0.1; Server = localhost; Port = 5432; Database = library_server; UserID = postgres; Password = hatem0199; Pooling = true");

                await conn.OpenAsync();

                await using var cmd = new NpgsqlCommand("INSERT INTO reviews (evaluation, mediaid, rating) VALUES (@evaluation, @mediaid, @rating)", conn)
                {
                    Parameters =
                    {
                        new("evaluation",newReview.evaluation),
                        new("mediaid", newReview.mediaid),
                        new("rating",newReview.rating )
                    }
                };
                await using var reader = await cmd.ExecuteReaderAsync();

            }
            else
            {
                newReview.mediaid = 99;
            }
            return View(newReview);
        }
    }
}
