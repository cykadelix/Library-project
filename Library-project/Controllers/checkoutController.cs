using Library_project.ViewModels.Checkout;
using Library_project.Data.Enums;
using Library_project.Data.Objects;
using Library_project.Models;
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
            return View();
        }

        public IActionResult createCheckout()
        {
            var newCheckout = new CreateCheckoutViewModel();
            return View(newCheckout);
        }

        [HttpPost]


        public async Task<IActionResult> CreateCheckoutLandingPage(CreateCheckoutViewModel newCheckout)
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(_config.GetConnectionString("local_lib"));
            var newCheckout2 = new CreateCheckoutViewModel();
            if (ModelState.IsValid)
            {

                await conn.OpenAsync();
                try
                {
                    await using var cmd = new NpgsqlCommand("INSERT INTO checkouts (studentid, mediaid, checkoutdate,returndate, checkoutid, returned) VALUES (@studentid, @mediaid, current_timestamp, current_timestamp + INTERVAL '1 month', DEFAULT, DEFAULT)", conn)
                    {
                        Parameters =
                        {
                            new("studentid",newCheckout.studentid),
                            new("mediaid", newCheckout.mediaid)
                        }
                    };
                    await using var reader = await cmd.ExecuteReaderAsync();
                }
                catch(Exception ex)
                {

                    TempData["AlertMessage"] = ex.Message;
                    return View(newCheckout2);
                }
            }   
            else
            {
                newCheckout.studentid = -1;
            }
			
			return View(newCheckout2);
        }

		public async Task<IActionResult> CheckoutList()
		{

			var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));

			await using var dataSource = dataSourceBuilder.Build();
			await using var command = dataSource.CreateCommand("SELECT * FROM checkouts");
			await using var reader = await command.ExecuteReaderAsync();

			var checkoutList = new CheckoutListViewModel();
			var LocalList = new List<checkouts>();
			while (await reader.ReadAsync())
			{
				LocalList.Add(new checkouts()
				{
					checkoutid = (int)reader["checkoutid"],
					studentid = (int)reader["studentid"] ,
					mediaid = (int)reader["mediaid"],
					checkoutdate = (DateTime)reader["checkoutdate"],
					returndate = (DateTime)reader["returndate"]
				
					//publicdate = reader.GetFieldValue<DateOnly>(3),
				
				});

				checkoutList.allCheckouts = LocalList;
			}


			return View(checkoutList);

		}
	}
}
