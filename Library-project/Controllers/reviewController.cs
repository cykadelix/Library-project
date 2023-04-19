using Library_project.Models;
using Library_project.ViewModels.Checkout;
using Library_project.ViewModels.review;
using Library_project.ViewModels.Review;
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
            var newReview = new reviewViewModel();
            return View(newReview);
        }


        public async Task<IActionResult> CreateReviewLandingPage(reviewViewModel newReview)
        {
            if (ModelState.IsValid)
            {
                await using NpgsqlConnection conn = new NpgsqlConnection("Host = 127.0.0.1; Server = localhost; Port = 5432; Database = library_server; UserID = postgres; Password = hatem0199; Pooling = true");

                await conn.OpenAsync();

                await using var cmd = new NpgsqlCommand("INSERT INTO reviews (reviewid, evaluation, mediaid, rating, studentid) VALUES (DEFAULT, @evaluation, @mediaid, @rating, @studentid)", conn)
                {
                    Parameters =
                    {
                        new("evaluation",newReview.evaluation),
                        new("mediaid", newReview.mediaid),
                        new("rating",newReview.rating ),
                        new("studentid", newReview.studentid)
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

        public async Task<IActionResult> ReviewList()
        {

            var dataSourceBuilder = new NpgsqlDataSourceBuilder("Host = 127.0.0.1; Server = localhost; Port = 5432; Database = library_server; UserID = postgres; Password = hatem0199; Pooling = true");

            await using var dataSource = dataSourceBuilder.Build();
            await using var command = dataSource.CreateCommand("SELECT * FROM reviews");
            await using var reader = await command.ExecuteReaderAsync();

            var revList = new ReviewListViewModel();
            var LocalList = new List<reviews>();
            while (await reader.ReadAsync())
            {
                LocalList.Add(new reviews()
                {
                    mediaid = (int)reader["mediaid"],
                    studentid = (int)reader["studentid"],
                    evaluation = (string)reader["evaluation"],
                    rating = (short)reader["rating"]
                     

                    //publicdate = reader.GetFieldValue<DateOnly>(3),

                });

                revList.allReviews = LocalList;
            }


            return View(revList);

        }
    }
}
