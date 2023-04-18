using Library_project.Models;
using Library_project.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Library_project.Controllers
{
    public class DashboardSettingsController : Controller
    {
        private readonly IConfiguration _config;

        public DashboardSettingsController(IConfiguration config)
        {
            _config = config;
        }

        [Route("DashboardSettings/Admin/{id:int}")]
        public IActionResult Admin(int id)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));

            using var dataSource = dataSourceBuilder.Build();
            using var command = dataSource.CreateCommand("SELECT * FROM employees WHERE employeeid ='" + id + "'");
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
            }

            return View(localemployee);
        }

        [HttpPost]
        public IActionResult UpdateAdmin(CreateEmployeeViewModel model)
        {
            using (var conn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))
            {
                conn.Open();
                string queryParameters = "fname=@f1, mname=@m1, lname=@l1, position=@p1, age=@a1, email=@e1, password=@p2, homeaddress=@h1, phonenumber=@p3 ";
                string updateCommand = "UPDATE employees SET " + queryParameters + "WHERE employeeid='" + model.employeeid + "'";
                using (var command = new NpgsqlCommand(updateCommand, conn))
                {
                    command.Parameters.AddWithValue("f1", model.fname);
                    command.Parameters.AddWithValue("m1", (model.mname == null ? "" : model.mname));
                    command.Parameters.AddWithValue("l1", model.lname);
                    command.Parameters.AddWithValue("p1", model.position);
                    command.Parameters.AddWithValue("a1", model.age);
                    command.Parameters.AddWithValue("e1", model.email);
                    command.Parameters.AddWithValue("p2", model.password);
                    command.Parameters.AddWithValue("h1", model.homeaddress);
                    command.Parameters.AddWithValue("p3", model.phonenumber);

                    int nRows = command.ExecuteNonQuery();

                }
            }
            return View("~/Views/DashboardSettings/Admin.cshtml", model);
        }

        [Route("DashboardSettings/Student/{id:int}")]
        public IActionResult Student(int id)
        {
            return View();
        }
    }
}
