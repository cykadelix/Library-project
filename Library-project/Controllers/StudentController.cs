using Library_project.Models;
using Library_project.ViewModels.Student;
using Npgsql;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library_project.Controllers
{
    public class StudentController : Controller
    {
        private readonly IConfiguration _config;
        public StudentController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult StudentIndex()
        {
            return View();
        }

        public List<CreateStudentViewModel>? StudentToList()
        {

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));
            using var dataSource = dataSourceBuilder.Build();
            using var command = dataSource.CreateCommand("SELECT * FROM students");
            using var reader = command.ExecuteReader();

            var LocalList = new List<CreateStudentViewModel>();
            while (reader.Read())
            {
                LocalList.Add(new CreateStudentViewModel()
                {
                    fname = (string)reader["fname"],
                    mname = (string)reader["mname"],
                    lname = (string)reader["lname"],
                    library_card_number = (int)reader["library_card_number"],
                    email = (string)reader["email"],
                    password = (string)reader["password"],
                    age = (int)reader["age"],
                    phonenumber = (string)reader["phonenumber"],
                    homeaddress = (string)reader["homeaddress"],
                    overdue_fees = (int)reader["overduefees"],
                });
            }
            if (LocalList.Count == 0)
            {
                return null;
            }
            return LocalList;
        }
        [HttpGet]
        public IActionResult GetStudentList()
        {
            return Json(StudentToList());
        }

        [HttpPost]
        public IActionResult CreateStudent(CreateStudentViewModel model)
        {
            if (model.fname == null)
            {
                return View("~/Views/Student/StudentIndex.cshtml");
            }
            else if (ModelState.IsValid)
            {
                using NpgsqlConnection conn = new NpgsqlConnection(_config.GetConnectionString("local_lib"));
                // Connect to the database
                conn.Open();

                var checkDuplicate = "SELECT COUNT(library_card_number) FROM students WHERE email='" + model.email + "'";
                using (var com = new NpgsqlCommand(checkDuplicate, conn))
                {
                    int duplicate = 0;
                    var read = com.ExecuteReader();
                    if (read.Read())
                    {
                        duplicate = read.GetInt32(0);
                    }
                    if (duplicate != 0)
                    {
                        model.errorMessage = "Duplicate email found. Please use a different email.";
                        return View("~/Views/Student/StudentIndex.cshtml", model);
                    }
                    read.Close();
                }


                using var command = new NpgsqlCommand("INSERT INTO students (VALUES(" +
                    "DEFAULT, @fname, @mname, @lname, @homeaddress, @email, @password, @phonenumber, @overdue_fees, @age))", conn)
                {
                    Parameters =
                        {
                            new("fname", model.fname),
                            new("mname", (model.mname == null? "": model.mname)),
                            new("lname", model.lname),
                            new("homeaddress", model.homeaddress),
                            new("email", model.email),
                            new("password", model.password),
                            new("phonenumber", model.phonenumber),
                            new("overdue_fees", model.overdue_fees),
                            new("age", model.age)
                        }
                };
                using var reader = command.ExecuteReader();
            }
            return View("~/Views/Student/StudentIndex.cshtml");
        }

        [HttpGet]
        public IActionResult EditStudent(int lcn)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));

            using var dataSource = dataSourceBuilder.Build();
            using var command = dataSource.CreateCommand("SELECT * FROM students where library_card_number='" + lcn + "'");
            using var reader = command.ExecuteReader();

            var localStudent = new CreateStudentViewModel();
            while (reader.Read())
            {
                localStudent.library_card_number = reader.GetFieldValue<int>(0);
                localStudent.fname = reader.GetFieldValue<string>(1);
                localStudent.mname = reader.GetFieldValue<string>(2);
                localStudent.lname = reader.GetFieldValue<string>(3);
                localStudent.homeaddress = reader.GetFieldValue<string>(4);
                localStudent.email = reader.GetFieldValue<string>(5);
                localStudent.password = reader.GetFieldValue<string>(6);
                localStudent.phonenumber = reader.GetFieldValue<string>(7);
                localStudent.overdue_fees = reader.GetFieldValue<int>(8);
                localStudent.age = reader.GetFieldValue<int>(9);
            }

            return View("~/Views/Student/StudentIndex.cshtml", localStudent);
        }

        [HttpPost]
        public IActionResult UpdateStudent(CreateStudentViewModel model)
        {
            using (var conn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))
            {
                conn.Open();
                string queryParameters = "fname=@f1, mname=@m1, lname=@l1, email=@e1, password=@p1, age=@a1, homeaddress=@h1, phonenumber=@p2, overduefees=@o1 ";
                string updateCommand = "UPDATE students SET " + queryParameters + "WHERE library_card_number='" + model.library_card_number + "'";
                using (var command = new NpgsqlCommand(updateCommand, conn))
                {
                    command.Parameters.AddWithValue("f1", model.fname);
                    command.Parameters.AddWithValue("m1", (model.mname == null ? "" : model.mname));
                    command.Parameters.AddWithValue("l1", model.lname);
                    command.Parameters.AddWithValue("e1", model.email);
                    command.Parameters.AddWithValue("p1", model.password);
                    command.Parameters.AddWithValue("a1", model.age);
                    command.Parameters.AddWithValue("h1", model.homeaddress);
                    command.Parameters.AddWithValue("p2", model.phonenumber);
                    command.Parameters.AddWithValue("o1", model.overdue_fees);

                    int nRows = command.ExecuteNonQuery();

                }
            }

            return View("~/Views/Student/StudentIndex.cshtml");
        }

        [HttpDelete]
        public IActionResult DeleteStudent(int lcn)
        {
            using (var conn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))
            {
                conn.Open();

                var sqlCommand = "DELETE FROM students WHERE library_card_number='" + lcn.ToString() + "';";
                using (var command = new NpgsqlCommand(sqlCommand, conn))
                {
                    int nRows = command.ExecuteNonQuery();
                }
            }
            return new JsonResult(new { redirectToUrl = Url.Action("StudentIndex", "Student") });
        }

        public IActionResult StudentCheckouts()
        {
            return View();
        }

        public IActionResult StudentBalance()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PayBalance(StudentBalanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var conn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))

                {
                    conn.Open();

                    using var cmd = new NpgsqlCommand("UPDATE students SET overduefees = overduefees - @a WHERE library_card_number = 13", conn)
                    {
                        Parameters =
                        {
                            new("a", model.amount),

                        }
                    };
                    using var reader = cmd.ExecuteReader();

                }

            }
            return View("~/Views/Student/StudentBalance.cshtml");

        }
    }
}
