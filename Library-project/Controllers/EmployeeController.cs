using Library_project.Data.Enums;
using Library_project.Data.Objects;
using Library_project.Models;
using Library_project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Npgsql;

namespace Library_project.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly string connString = "Host=127.0.0.1;Server=localhost;Port=5432;Database=my_server;UserID=postgres;Password=Fuentes5;Pooling=true";



        public async Task<IActionResult> Index()
        {

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connString);
            await using var dataSource = dataSourceBuilder.Build();
            await using var command = dataSource.CreateCommand("SELECT * FROM employee");
            await using var reader = await command.ExecuteReaderAsync();

            var employeeList = new listEmployeeViewModel();
            var LocalList = new List<Employee>();
            while (await reader.ReadAsync())
            {
                LocalList.Add(new Employee()
                {
                    fName = (string)reader["fname"],
                    mName = (string)reader["mname"],
                    lName = (string)reader["lName"],
                    employeeID = (int)reader["employeeID"],
                    supervisorID = (int)reader["supervisorID"],
                    position = (string)reader["position"],
                    salary = (float)reader["salary"],
                    age = (short)reader["age"],
                    eMail = (string)reader["eMail"],
                    password = (string)reader["password"],
                    homeAddress = (string)reader["homeAddress"],
                    phoneNumber = (string)reader["phoneNumber"],
                    supervisor = (Employee)reader["supervisor"]
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
            var newEmployee = new CreateEmployeeViewModel();

            return View(newEmployee);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployeeLandingPage(CreateEmployeeViewModel newEmployee)
        {
            CreateEmployeeViewModel example = new CreateEmployeeViewModel();
            example.fName = newEmployee.fName;
            example.mName = newEmployee.mName;
            example.lName = newEmployee.lName;
            example.employeeID = newEmployee.employeeID;
            example.supervisorID = newEmployee.supervisorID;
            example.position = newEmployee.position;
            example.salary = newEmployee.salary;
            example.age = newEmployee.age;
            example.eMail = newEmployee.eMail;
            example.password = newEmployee.password;
            example.homeAddress = newEmployee.homeAddress;
            example.phoneNumber = newEmployee.phoneNumber;
            example.supervisor = newEmployee.supervisor;

            if (ModelState.IsValid)
            {






                await using NpgsqlConnection conn = new NpgsqlConnection("Host=127.0.0.1;Server=localhost;Port=5432;Database=my_server;UserID=postgres;Password=Fuentes5;Pooling=true;Include Error Detail=true;");


                // Connect to the database
                await conn.OpenAsync();

                await using var command = new NpgsqlCommand("INSERT INTO employee(VALUES(" +
                    "@fName, @mName, @lName, DEFAULT, DEFAULT, @phoneNumber, @email, @homeAddress, @position, @salary, @age, @password))", conn)
                {
                    Parameters =
                        {
                            new("fName", newEmployee.fName),
                            new("mName", newEmployee.mName),
                            new("lName", newEmployee.lName),
                            new("employeeID", newEmployee.employeeID),
                            new("supervisorID", newEmployee.supervisorID),
                            new("position", newEmployee.position),
                            new("salary", newEmployee.salary),
                            new("age", newEmployee.age),
                            new("eMail", newEmployee.eMail),
                            new("password", newEmployee.password),
                            new("homeAddress", newEmployee.homeAddress),
                            new("phoneNumber", newEmployee.phoneNumber),
                            new("supervisor", newEmployee.supervisor),
                        }
                };
                await using var reader = await command.ExecuteReaderAsync();








            }
            else
            {

                example.fName = "invalid";

            }
            return View(example);


        }





        public async Task<IActionResult> Edit(int employee_id)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connString);

            await using var dataSource = dataSourceBuilder.Build();
            await using var command = dataSource.CreateCommand("SELECT * FROM employee");
            await using var reader = await command.ExecuteReaderAsync();




            var localEmployee = new EditEmployeeViewModel();
            reader.Read();
            using var innerRead = reader.GetData(0);

            while (innerRead.Read())
            {
                localEmployee.fName = innerRead.GetFieldValue<string>(1);
                localEmployee.mName = innerRead.GetFieldValue<string>(2);
                localEmployee.lName = innerRead.GetFieldValue<string>(3);
                localEmployee.employeeID = innerRead.GetFieldValue<int>(4);
                localEmployee.supervisorID = innerRead.GetFieldValue<int>(5);
                localEmployee.position = innerRead.GetFieldValue<string>(6);
                localEmployee.salary = innerRead.GetFieldValue<float>(7);
                localEmployee.age = innerRead.GetFieldValue<short>(7);
                localEmployee.eMail = innerRead.GetFieldValue<string>(7);
                localEmployee.password = innerRead.GetFieldValue<string>(7);
                localEmployee.homeAddress = innerRead.GetFieldValue<string>(7);
                localEmployee.phoneNumber = innerRead.GetFieldValue<string>(7);
                localEmployee.supervisor = innerRead.GetFieldValue<Employee>(7);
            }

            return View(localEmployee);


        }
        public async Task<IActionResult> Edit(EditEmployeeViewModel editEmployeevm)
        {

            return RedirectToAction("Index");
        }
    }
}
