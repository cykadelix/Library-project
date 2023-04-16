using Library_project.Data.Enums;
using Library_project.Data.Objects;
using Library_project.Models;
using Library_project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Npgsql;

namespace Library_project.Controllers
{
    public class employeeController : Controller
    {
        private readonly string connString = "Host=127.0.0.1;Server=localhost;Port=5432;Database=my_library;UserID=postgres;Password=killer89;Pooling=true";



        public async Task<IActionResult> Index()
        {
            

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connString);
            await using var dataSource = dataSourceBuilder.Build();
            await using var command = dataSource.CreateCommand("SELECT * FROM employee");
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
                    employeeid = (int)reader["employeeID"],
                    
                    position = (string)reader["position"],
                    salary = (float)reader["salary"],
                    birthday = (DateOnly)reader["birthday"],
                    email = (string)reader["eMail"],
                    password = (string)reader["password"],
                    homeaddress = (string)reader["homeaddress"],
                    phonenumber = (string)reader["phoneNumber"],
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
        public IActionResult CreateemployeeView()
        {
            var newemployee = new CreateEmployeeViewModel();

            return View(newemployee);
        }
        [HttpPost]
        public async Task<IActionResult> CreateemployeeLandingPage(CreateEmployeeViewModel newemployee)
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
            example.supervisor = newemployee.supervisor;

            if (ModelState.IsValid)
            {






                await using NpgsqlConnection conn = new NpgsqlConnection("Host=127.0.0.1;Server=localhost;Port=5432;Database=my_server;UserID=postgres;Password=Fuentes5;Pooling=true;Include Error Detail=true;");


                // Connect to the database
                await conn.OpenAsync();

                await using var command = new NpgsqlCommand("INSERT INTO employee(VALUES(" +
                    "@fname, @mname, @lname, DEFAULT, DEFAULT, @phoneNumber, @email, @homeaddress, @position, @salary, @age, @password))", conn)
                {
                    Parameters =
                        {
                            new("fname", newemployee.fname),
                            new("mname", newemployee.mname),
                            new("lname", newemployee.lname),
                            new("position", newemployee.position),
                            new("salary", newemployee.salary),
                            new("age", newemployee.age),
                            new("eMail", newemployee.email),
                            new("password", newemployee.password),
                            new("homeaddress", newemployee.homeaddress),
                            new("phoneNumber", newemployee.phonenumber),
                            new("supervisor", newemployee.supervisor),
                        }
                };
                await using var reader = await command.ExecuteReaderAsync();








            }
            else
            {

                example.fname = "invalid";

            }
            return View(example);


        }





        public async Task<IActionResult> Edit(int employee_id)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connString);

            await using var dataSource = dataSourceBuilder.Build();
            await using var command = dataSource.CreateCommand("SELECT * FROM employee");
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
