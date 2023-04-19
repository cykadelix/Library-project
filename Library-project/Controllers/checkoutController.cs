using Library_project.ViewModels.Checkout;
using Library_project.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
namespace Library_project.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public CheckoutController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateCheckout()
        {
            var newCheckout = new CheckoutViewModel();
            return View(newCheckout);
        }
        
        [HttpPost]
        public string CreateCheckout(CheckoutViewModel newCheckout)
        {
            if (ModelState.IsValid)
            {
                using NpgsqlConnection conn = new NpgsqlConnection(_config.GetConnectionString("local_lib"));

                conn.Open();

                try
                {
                    using var cmd = new NpgsqlCommand("INSERT INTO checkouts (studentid, mediaid, checkoutdate,returndate, checkoutid, returned) VALUES (@studentid, @mediaid, current_timestamp, current_timestamp + INTERVAL '1 month', DEFAULT, DEFAULT)", conn)
                    {
                        Parameters =
                        {
                            new("studentid", newCheckout.studentid),
                            new("mediaid", newCheckout.mediaid)
                        }
                    };
                    using var reader = cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

            }   
			return "";
        }

		public List<checkouts>? CheckoutList()
		{

			var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));

			using var dataSource = dataSourceBuilder.Build();
			using var command = dataSource.CreateCommand("SELECT * FROM checkouts");
			using var reader = command.ExecuteReader();

			var checkoutList = new CheckoutListViewModel();
			var LocalList = new List<checkouts>();
			while (reader.Read())
			{
                LocalList.Add(new checkouts()
                {
                    checkoutid = (int)reader["checkoutid"],
                    studentid = (int)reader["studentid"],
                    mediaid = (int)reader["mediaid"],
                    checkoutdate = (DateTime)reader["checkoutdate"],
                    returndate = (DateTime)reader["returndate"],
                    returned = (bool)reader["returned"],
                    returned_date = (reader.IsDBNull(6) ? "" : reader.GetDateTime(6).ToString())
                }) ;

				checkoutList.allCheckouts = LocalList;
			}
            if (LocalList.Count == 0) return null;

			return LocalList;
		}

        public List<StudentCheckoutsViewModel>? StudentCheckoutList(int studentid)
        {

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));

            using var dataSource = dataSourceBuilder.Build();
            string comm = "SELECT null as brand, null as serialnumber, title, checkoutdate, returndate, returned, returned_date, 'Book' as mediatype " +
                "FROM checkouts, books " +
                "WHERE ( checkouts.studentid = '" + studentid + "' AND checkouts.mediaid = books.mediaid) UNION " +

                "SELECT brand, serialnumber, null as title, checkoutdate, returndate, returned, returned_date, 'Camera' as mediatype " +
                "FROM checkouts, cameras " +
                "WHERE ( checkouts.studentid = '" + studentid + "' AND checkouts.mediaid = cameras.mediaid) UNION " +

                "SELECT brand, serialnumber, null as title, checkoutdate, returndate, returned, returned_date, 'Computer' as mediatype " +
                "FROM checkouts, computers " +
                "WHERE ( checkouts.studentid = '" + studentid + "' AND checkouts.mediaid = computers.mediaid) UNION " +

                "SELECT null as brand, null as serialnumber, title, checkoutdate, returndate, returned, returned_date, 'Audiobook' as mediatype " +
                "FROM checkouts, audiobooks " +
                "WHERE ( checkouts.studentid = '" + studentid + "' AND checkouts.mediaid = audiobooks.mediaid) UNION " +

                "SELECT null as brand, null as serialnumber, title, checkoutdate, returndate, returned, returned_date, 'Journal' as mediatype " +
                "FROM checkouts, journals " +
                "WHERE ( checkouts.studentid = '" + studentid + "' AND checkouts.mediaid = journals.mediaid) UNION " +

                "SELECT null as brand, null as serialnumber, title, checkoutdate, returndate, returned, returned_date, 'Movie' as mediatype " +
                "FROM checkouts, movies " +
                "WHERE ( checkouts.studentid = '" + studentid + "' AND checkouts.mediaid = movies.mediaid) UNION " +

                "SELECT brand, serialnumber, null as title, checkoutdate, returndate, returned, returned_date, 'Projector' as mediatype " +
                "FROM checkouts, projectors " +
                "WHERE ( checkouts.studentid = '" + studentid + "' AND checkouts.mediaid = projectors.mediaid)";

            using var command = dataSource.CreateCommand(comm);
            using var reader = command.ExecuteReader();

            var LocalList = new List<StudentCheckoutsViewModel>();
            while (reader.Read())
            {
                LocalList.Add(new StudentCheckoutsViewModel()
                {
                    Brand = reader.IsDBNull(0) ? "" : reader.GetString(0),
                    SerialNo = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    Title = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    CheckoutDate = reader.GetDateTime(3).ToString("MM-dd-yyyy HH:mm"),
                    ReturnDate = reader.GetDateTime(4).ToString("MM-dd-yyyy HH:mm"),
                    Returned = reader.IsDBNull(5) ? false : reader.GetBoolean(5),
                    ReturnedDate = reader.IsDBNull(6) ? "" : reader.GetDateTime(6).ToString(),
                    MediaType = reader.GetString(7)
                });
            }
            if (LocalList.Count == 0) return null;

            return LocalList;
        }

        [HttpGet]
        public IActionResult GetStudentsCheckoutList(int studentid)
        {
            return Json(StudentCheckoutList(studentid));
        }

        [HttpGet]
        public IActionResult GetCheckoutList()
        {
            return Json(CheckoutList());
        }


	}
}
