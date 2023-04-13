using Library_project.ViewModels.review;
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
            var newReview = new reviewViewModel();
            return View(newReview);
        }

    }
}
