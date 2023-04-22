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
        public string CreateCheckout(CreateCheckoutViewModel newCheckout)
        {
            if (ModelState.IsValid)
            {
                using NpgsqlConnection conn = new NpgsqlConnection(_config.GetConnectionString("local_lib"));

                conn.Open();

                try
                {
                    if (TempData.Peek("role") == "student")
                    {
                        using var cmd = new NpgsqlCommand("INSERT INTO checkouts (studentid, mediaid, checkoutdate,returndate, checkoutid, returned, employeeid) VALUES (@id, @mediaid, current_timestamp, current_timestamp + INTERVAL '1 month', DEFAULT, DEFAULT, -1)", conn)
                        {
                            Parameters =
                            {
                                new("id",newCheckout.id),
                                new("mediaid", newCheckout.mediaid)
                            }
                        };
                        using var reader = cmd.ExecuteReader();
                    }
                    else
                    {
                        using var cmd = new NpgsqlCommand("INSERT INTO checkouts (studentid, mediaid, checkoutdate,returndate, checkoutid, returned,employeeid) VALUES (-1, @mediaid, current_timestamp, current_timestamp + INTERVAL '1 month', DEFAULT, DEFAULT, @id)", conn)
                        {
                            Parameters =
                            {
                                new("id",newCheckout.id),
                                new("mediaid", newCheckout.mediaid)
                            }
                        };
                        using var reader = cmd.ExecuteReader();
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

            }
            return "";
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
                    studentid = (int)reader["studentid"],
                    mediaid = (int)reader["mediaid"],
                    checkoutdate = (DateTime)reader["checkoutdate"],
                    returndate = (DateTime)reader["returndate"]
                });

                checkoutList.allCheckouts = LocalList;
            }
            return View(checkoutList);
        }

        [HttpGet]
        public IActionResult GetCheckoutList()
        {
            return Json(CheckoutList());
        }
        public List<StudentCheckoutsViewModel>? StudentCheckoutList(int studentid)
        {

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));

            using var dataSource = dataSourceBuilder.Build();
            string comm = "SELECT null as brand, null as serialnumber, title, checkoutdate, returndate, returned, returned_date, 'Book' as mediatype, checkouts.mediaid " +
                "FROM checkouts, books " +
                "WHERE ( checkouts.studentid = '" + studentid + "' AND checkouts.mediaid = books.mediaid) UNION " +

                "SELECT brand, serialnumber, null as title, checkoutdate, returndate, returned, returned_date, 'Camera' as mediatype, checkouts.mediaid " +
                "FROM checkouts, cameras " +
                "WHERE ( checkouts.studentid = '" + studentid + "' AND checkouts.mediaid = cameras.mediaid) UNION " +

                "SELECT brand, serialnumber, null as title, checkoutdate, returndate, returned, returned_date, 'Computer' as mediatype, checkouts.mediaid " +
                "FROM checkouts, computers " +
                "WHERE ( checkouts.studentid = '" + studentid + "' AND checkouts.mediaid = computers.mediaid) UNION " +

                "SELECT null as brand, null as serialnumber, title, checkoutdate, returndate, returned, returned_date, 'Audiobook' as mediatype, checkouts.mediaid " +
                "FROM checkouts, audiobooks " +
                "WHERE ( checkouts.studentid = '" + studentid + "' AND checkouts.mediaid = audiobooks.mediaid) UNION " +

                "SELECT null as brand, null as serialnumber, title, checkoutdate, returndate, returned, returned_date, 'Journal' as mediatype, checkouts.mediaid " +
                "FROM checkouts, journals " +
                "WHERE ( checkouts.studentid = '" + studentid + "' AND checkouts.mediaid = journals.mediaid) UNION " +

                "SELECT null as brand, null as serialnumber, title, checkoutdate, returndate, returned, returned_date, 'Movie' as mediatype, checkouts.mediaid " +
                "FROM checkouts, movies " +
                "WHERE ( checkouts.studentid = '" + studentid + "' AND checkouts.mediaid = movies.mediaid) UNION " +

                "SELECT brand, serialnumber, null as title, checkoutdate, returndate, returned, returned_date, 'Projector' as mediatype, checkouts.mediaid " +
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
                    MediaType = reader.GetString(7),
                    mediaId = reader.GetInt32(8),
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

        public List<StudentCheckoutsViewModel>? EmployeeCheckoutList(int employeeid)
        {

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));

            using var dataSource = dataSourceBuilder.Build();
            string comm = "SELECT null as brand, null as serialnumber, title, checkoutdate, returndate, returned, returned_date, 'Book' as mediatype, checkouts.mediaid " +
                "FROM checkouts, books " +
                "WHERE ( checkouts.employeeid = '" + employeeid + "' AND checkouts.mediaid = books.mediaid) UNION " +

                "SELECT brand, serialnumber, null as title, checkoutdate, returndate, returned, returned_date, 'Camera' as mediatype, checkouts.mediaid " +
                "FROM checkouts, cameras " +
                "WHERE ( checkouts.employeeid = '" + employeeid + "' AND checkouts.mediaid = cameras.mediaid) UNION " +

                "SELECT brand, serialnumber, null as title, checkoutdate, returndate, returned, returned_date, 'Computer' as mediatype, checkouts.mediaid " +
                "FROM checkouts, computers " +
                "WHERE ( checkouts.employeeid = '" + employeeid + "' AND checkouts.mediaid = computers.mediaid) UNION " +

                "SELECT null as brand, null as serialnumber, title, checkoutdate, returndate, returned, returned_date, 'Audiobook' as mediatype, checkouts.mediaid " +
                "FROM checkouts, audiobooks " +
                "WHERE ( checkouts.employeeid = '" + employeeid + "' AND checkouts.mediaid = audiobooks.mediaid) UNION " +

                "SELECT null as brand, null as serialnumber, title, checkoutdate, returndate, returned, returned_date, 'Journal' as mediatype, checkouts.mediaid " +
                "FROM checkouts, journals " +
                "WHERE ( checkouts.employeeid = '" + employeeid + "' AND checkouts.mediaid = journals.mediaid) UNION " +

                "SELECT null as brand, null as serialnumber, title, checkoutdate, returndate, returned, returned_date, 'Movie' as mediatype, checkouts.mediaid " +
                "FROM checkouts, movies " +
                "WHERE ( checkouts.employeeid = '" + employeeid + "' AND checkouts.mediaid = movies.mediaid) UNION " +

                "SELECT brand, serialnumber, null as title, checkoutdate, returndate, returned, returned_date, 'Projector' as mediatype, checkouts.mediaid " +
                "FROM checkouts, projectors " +
                "WHERE ( checkouts.employeeid = '" + employeeid + "' AND checkouts.mediaid = projectors.mediaid)";

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
                    MediaType = reader.GetString(7),
                    mediaId = reader.GetInt32(8),
                });
            }
            if (LocalList.Count == 0) return null;

            return LocalList;
        }

        [HttpGet]
        public IActionResult GetEmployeesCheckoutList(int employeeid)
        {
            return Json(EmployeeCheckoutList(employeeid));
        }

        [HttpGet]
        public IActionResult CheckoutItem(int mediaId)
        {
            if (ModelState.IsValid)
            {
                using NpgsqlConnection conn = new NpgsqlConnection(_config.GetConnectionString("local_lib"));
                conn.Open();
                string command = "UPDATE checkouts SET returned = true, returned_date = CURRENT_TIMESTAMP(0)" +
                    "WHERE mediaid=@m1";
                using var cmd = new NpgsqlCommand(command, conn)
                {
                    Parameters =
                        {
                            new("m1", mediaId)
                        }
                };
                using var reader = cmd.ExecuteReader();
            }
            return View("~/Views/Student/StudentCheckouts.cshtml");
        }
    }
}
