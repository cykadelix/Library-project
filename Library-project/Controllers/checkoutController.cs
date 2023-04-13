using Library_project.ViewModels.checkout;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Npgsql;

namespace Library_project.Controllers
{
    public class checkoutController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public checkoutController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public IActionResult Index()
        {
            var newCheckout = new checkoutViewModel();
            return View(newCheckout);
        }

        [HttpPost]

        public async Task<IActionResult> CreateCheckOutLandingPage(checkoutViewModel newCheckout)
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(_config["ConnectionString"]);

            await conn.OpenAsync();
            await using var cmd = new NpgsqlCommand("INSERT INTO checkout (checkoutid, returndate,.... " +
            " VALUES(DEFAULT, '2000-12-07', @studentid ))", conn)
            {
                Parameters =
                {
                    new("sturdentid",newCheckout.studentid)
                }
            };
            await using var reader = await cmd.ExecuteReaderAsync();
            return View(newCheckout);
        }
    }
}
