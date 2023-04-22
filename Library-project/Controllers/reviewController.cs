using Library_project.ViewModels.Review;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Library_project.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public ReviewController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult CreateReview()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateReview(ReviewViewModel newReview)
        {
            if (ModelState.IsValid)
            {
                using NpgsqlConnection conn = new NpgsqlConnection(_config.GetConnectionString("local_lib"));

                conn.Open();

                using var cmd = new NpgsqlCommand("INSERT INTO reviews (reviewid, description, rating, mediaid, studentid, employeeid) VALUES (DEFAULT, @d1, @r1, @m1, @s1, @e1)", conn)
                {
                    Parameters =
                    {
                        new("d1",newReview.description),
                        new("r1", newReview.mediaid),
                        new("m1",newReview.mediaid ),
                        new("s1",newReview.studentid ),
                        new("e1",newReview.employeeid ),
                    }
                };
                try
                {
                    using var reader = cmd.ExecuteReader();
                }
                catch (Exception ex) 
                {
                    newReview.resultmessage = "Review was not submitted. Please check the mediaid.";
                    return View(newReview);
                }
            }
            newReview.resultmessage = "Review submitted successfully";
            return View(newReview);
        }
    }
}
