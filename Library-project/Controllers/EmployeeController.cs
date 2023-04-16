using Library_project.Data.Enums;
using Library_project.Data.Objects;
using Library_project.Models;
using Library_project.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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


        public async Task<IActionResult> EmployeeIndex()
        {


            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));
            await using var dataSource = dataSourceBuilder.Build();
            await using var command = dataSource.CreateCommand("SELECT * FROM employees");
            await using var reader = await command.ExecuteReaderAsync();

            var employeeList = new listEmployeeViewModel();
            var LocalList = new List<employees>();
            while (await reader.ReadAsync())
            {
                LocalList.Add(new employees()
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
                    salary = (float)reader["salary"]
                });

                employeeList.allEmployees = LocalList;
            }


            return View(employeeList);

        }
        public IActionResult employeeForm()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateEmployeeView()
        {
            var newemployee = new CreateEmployeeViewModel();

            return View(newemployee);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployeeLandingPage(CreateEmployeeViewModel newemployee)
        {
            CreateEmployeeViewModel example = new CreateEmployeeViewModel();
            example.fname = newemployee.fname;
            example.mname = newemployee.mname;
            example.lname = newemployee.lname;
            example.position = newemployee.position;
            example.salary = newemployee.salary;
            example.age = newemployee.age;
            example.email = newemployee.email;
            example.password = newemployee.password;
            example.homeaddress = newemployee.homeaddress;
            example.phonenumber = newemployee.phonenumber;

            await using NpgsqlConnection conn = new NpgsqlConnection(_config.GetConnectionString("local_lib"));
            // Connect to the database
            await conn.OpenAsync();

            await using var command = new NpgsqlCommand("INSERT INTO employees (VALUES(" +
                "DEFAULT, @fname, @mname, @lname, @position, @age, @email, @password, @homeaddress, @phonenumber, @salary))", conn)
            {
                Parameters =
                    {
                        new("fname", newemployee.fname),
                        new("mname", newemployee.mname),
                        new("lname", newemployee.lname),
                        new("position", newemployee.position),
                        new("salary", newemployee.salary),
                        new("age", newemployee.age),
                        new("email", newemployee.email),
                        new("password", newemployee.password),
                        new("homeaddress", newemployee.homeaddress),
                        new("phonenumber", newemployee.phonenumber)
                    }
            };
            await using var reader = await command.ExecuteReaderAsync();

            return View(example);
        }

        public async Task<IActionResult> Edit(int employee_id)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));

            await using var dataSource = dataSourceBuilder.Build();
            await using var command = dataSource.CreateCommand("SELECT * FROM employees");
            await using var reader = await command.ExecuteReaderAsync();

            var localemployee = new EditEmployeeViewModel();
            reader.Read();
            using var innerRead = reader.GetData(0);

            while (innerRead.Read())
            {
                localemployee.fname = innerRead.GetFieldValue<string>(1);
                localemployee.mname = innerRead.GetFieldValue<string>(2);
                localemployee.lname = innerRead.GetFieldValue<string>(3);
                localemployee.position = innerRead.GetFieldValue<string>(6);
                localemployee.salary = innerRead.GetFieldValue<float>(7);
                localemployee.age = innerRead.GetFieldValue<short>(7);
                localemployee.email = innerRead.GetFieldValue<string>(7);
                localemployee.password = innerRead.GetFieldValue<string>(7);
                localemployee.homeaddress = innerRead.GetFieldValue<string>(7);
                localemployee.phonenumber = innerRead.GetFieldValue<string>(7);
                localemployee.supervisor = innerRead.GetFieldValue<employees>(7);
            }

            return View(localemployee);
        }
        public async Task<IActionResult> Edit(EditEmployeeViewModel editemployeevm)
        {

            return RedirectToAction("Index");
        }
    }
}
