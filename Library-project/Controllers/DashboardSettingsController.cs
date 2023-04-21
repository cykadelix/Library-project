using Library_project.Models;
using Library_project.ViewModels.Employee;
using Library_project.ViewModels.Student;
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
            EditStudentVM myStudent = new EditStudentVM();
            int libCard = (int)TempData.Peek("libraryCard");
            using (var conn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM students WHERE library_card_number='" + id + "'", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        myStudent.library_card_number = reader.GetInt16(0);
                        myStudent.fname = reader.GetString(1);
                        myStudent.mname = reader.GetString(2);
                        myStudent.phonenumber = reader.GetString(7);
                        
                        myStudent.lname = reader.GetString(3);
                        myStudent.homeaddress = reader.GetString(4);
                        myStudent.age = reader.GetInt16(9);
                        myStudent.email = reader.GetString(5);
                        myStudent.password = reader.GetString(6);


                    }
                }

            }
            return View(myStudent);
        }
        [HttpPost]
        public IActionResult UpdateStudent(EditStudentVM newStudent)
        {
            using (var conn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))
            {
                conn.Open();
                string queryParms = "fname=@fname, mname=@mname, lname=@lname, homeaddress=@homeaddress, email=@email, password=@password, phonenumber=@phonenumber, age=@age";
                string updateCommand = "UPDATE students SET " + queryParms + " WHERE Library_card_number='" + newStudent.library_card_number + "'";
                using (var command=new NpgsqlCommand(updateCommand, conn))
                {
                    command.Parameters.AddWithValue("fname",newStudent.fname);
                    if(newStudent.mname != null)
                    {
                        command.Parameters.AddWithValue("mname", newStudent.mname);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("mname", "");
                    }
                    
                    command.Parameters.AddWithValue("lname",newStudent.lname);
                    command.Parameters.AddWithValue("homeaddress",newStudent.homeaddress);
                    command.Parameters.AddWithValue("email",newStudent.email);
                    command.Parameters.AddWithValue("password",newStudent.password);
                    command.Parameters.AddWithValue("phonenumber",newStudent.phonenumber);
                    command.Parameters.AddWithValue("age",newStudent.age);

                    command.ExecuteNonQuery();

                }
            }

            return View("~/Views/DashboardSettings/Student.cshtml", newStudent);
        }
    }
}
