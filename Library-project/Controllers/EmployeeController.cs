using Library_project.Models;
using Library_project.ViewModels.Employee;
using Npgsql;

namespace Library_project.Controllers
{
    public class employeeController : Controller
    {
        private readonly IConfiguration _config;
        public employeeController(IConfiguration config)
        {
            _config = config;
        }


        public IActionResult EmployeeIndex()
        {
            return View();
        }

        public IActionResult EmployeeCheckouts()
        {
            return View();
        }

        public List<CreateEmployeeViewModel>? EmployeeToList()
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));
            using var dataSource = dataSourceBuilder.Build();
            using var command = dataSource.CreateCommand("SELECT * FROM employees");
            using var reader = command.ExecuteReader();

            var employeeList = new List<CreateEmployeeViewModel>();
            while (reader.Read())
            {
                employeeList.Add(new CreateEmployeeViewModel()
                {
                    fname = (string)reader["fname"],
                    mname = (string)reader["mname"],
                    lname = (string)reader["lname"],
                    employeeid = (int)reader["employeeid"],
                    position = (string)reader["position"],
                    age = (short)reader["age"],
                    email = (string)reader["email"],
                    password = (string)reader["password"],
                    homeaddress = (string)reader["homeaddress"],
                    phonenumber = (string)reader["phonenumber"],
                    salary = (float)reader["salary"],
                    active = (bool)reader["active"],
                });
            }
            if(employeeList.Count == 0)
            {
                return null;
            }
            return employeeList;
        }

        [HttpGet]
        public IActionResult GetEmployeeList()
        {
            return Json(EmployeeToList());
        }


        [HttpPost]
        public IActionResult CreateEmployee(CreateEmployeeViewModel model)
        {
            if (model.fname == null)
            {
                return View("~/Views/Employee/EmployeeIndex.cshtml");
            }
            using NpgsqlConnection conn = new NpgsqlConnection(_config.GetConnectionString("local_lib"));
            // Connect to the database
            conn.Open();

            var checkDuplicate = "SELECT COUNT(employeeid) FROM employees WHERE email='" + model.email + "'";
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
                    return View("~/Views/Employee/EmployeeIndex.cshtml", model);
                }
                read.Close();
            }

            using var command = new NpgsqlCommand("INSERT INTO employees (VALUES(" +
                "DEFAULT, @fname, @mname, @lname, @position, @salary, @email, @password, @homeaddress, @phonenumber, @age, @active))", conn)
            {
                Parameters =
                    {
                        new("fname", model.fname),
                        new("mname", (model.mname == null? "" : model.fname)),
                        new("lname", model.lname),
                        new("position", model.position),
                        new("salary", model.salary),
                        new("age", model.age),
                        new("email", model.email),
                        new("password", model.password),
                        new("homeaddress", model.homeaddress),
                        new("phonenumber", model.phonenumber),
                        new("active", model.active)
                    }
            };
            using var reader = command.ExecuteReader();

            return View("~/Views/Employee/EmployeeIndex.cshtml");
        }

        [HttpGet]
        public IActionResult EditEmployee(int employeeId)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));

            using var dataSource = dataSourceBuilder.Build();
            using var command = dataSource.CreateCommand("SELECT * FROM employees WHERE employeeid ='" + employeeId + "'");
            using var reader = command.ExecuteReader();

            var localemployee = new CreateEmployeeViewModel();
            while (reader.Read())
            {
                localemployee.employeeid = reader.GetFieldValue<int>(0);
                localemployee.fname = reader.GetFieldValue<string>(1);
                localemployee.mname = reader.GetFieldValue<string>(2);
                localemployee.lname = reader.GetFieldValue<string>(3);
                localemployee.position = reader.GetFieldValue<string>(4);
                localemployee.salary = reader.GetFieldValue<float>(5);
                localemployee.email = reader.GetFieldValue<string>(6);
                localemployee.password = reader.GetFieldValue<string>(7);
                localemployee.homeaddress = reader.GetFieldValue<string>(8);
                localemployee.phonenumber = reader.GetFieldValue<string>(9);
                localemployee.age = reader.GetFieldValue<short>(10);
                localemployee.active = reader.GetFieldValue<bool>(11);
            }

            return View("~/Views/Employee/EmployeeIndex.cshtml", localemployee);
        }

        [HttpPost]
        public IActionResult UpdateEmployee(CreateEmployeeViewModel model)
        {
            using (var conn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))
            {
                conn.Open();
                string queryParameters = "fname=@f1, mname=@m1, lname=@l1, position=@p1, salary=@s1, age=@a1, email=@e1, password=@p2, homeaddress=@h1, phonenumber=@p3, active=@a2 ";
                string updateCommand = "UPDATE employees SET " + queryParameters + "WHERE employeeid='" + model.employeeid + "'";
                using (var command = new NpgsqlCommand(updateCommand, conn))
                {
                    command.Parameters.AddWithValue("f1", model.fname);
                    command.Parameters.AddWithValue("m1", model.mname == null ? "" : model.mname);
                    command.Parameters.AddWithValue("l1", model.lname);
                    command.Parameters.AddWithValue("p1", model.position);
                    command.Parameters.AddWithValue("s1", model.salary);
                    command.Parameters.AddWithValue("a1", model.age);
                    command.Parameters.AddWithValue("e1", model.email);
                    command.Parameters.AddWithValue("p2", model.password);
                    command.Parameters.AddWithValue("h1", model.homeaddress);
                    command.Parameters.AddWithValue("p3", model.phonenumber);
                    command.Parameters.AddWithValue("a2", model.active == null ? false : model.active);

                    int nRows = command.ExecuteNonQuery();

                }
            }
            return View("~/Views/Employee/EmployeeIndex.cshtml");
        }

        [HttpDelete]
        public IActionResult DeleteEmployee(int employeeId)
        {
            using (var conn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))
            {
                conn.Open();

                var sqlCommand = "UPDATE employees SET active=false WHERE employeeid='" + employeeId.ToString() + "';";
                using (var command = new NpgsqlCommand(sqlCommand, conn))
                {
                    int nRows = command.ExecuteNonQuery();
                }
            }
            return new JsonResult(new { redirectToUrl = Url.Action("EmployeeIndex", "Employee") });
        }
    }
}
