using Library_project.Models;
using Library_project.ViewModels.Student;
using Npgsql;
using System.Security.Claims;

namespace Library_project.Controllers
{
    public class AccountController : Controller
    {
        IConfiguration _config;
        public AccountController(IConfiguration config)
        {
            _config = config;
        }
        public IActionResult StudentRegistration()
        {
            RegisterStudentVM newStudent = new RegisterStudentVM();
            
            return View(newStudent);
        }
        public IActionResult RegisterStudentLandingPage(RegisterStudentVM student)
        {
            if (ModelState.IsValid)
            {
                using (var conn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))
                {
                    conn.Open();
                    var insertCommand = "INSERT INTO students (library_card_number, fname, mname, lname, homeaddress, email, password, phonenumber,overduefees,age)";
                    using (var command = new NpgsqlCommand(insertCommand + " VALUES(DEFAULT, @fname, @mname, @lname, @homeaddress, @email,@password, @phonenumber,0,@age)", conn))
                    {
                        
                        command.Parameters.AddWithValue("fname", student.fname);
                        command.Parameters.AddWithValue("mname", student.mname);
                        command.Parameters.AddWithValue("lname", student.lname);
                        command.Parameters.AddWithValue("homeaddress", student.homeaddress);
                        command.Parameters.AddWithValue("email", student.email);
                        command.Parameters.AddWithValue("password", student.password);
                        command.Parameters.AddWithValue("phonenumber", student.phonenumber);
                        command.Parameters.AddWithValue("phonenumber", student.phonenumber);
                        command.Parameters.AddWithValue("age", student.age);
                        command.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                student.fname = "fake name";
            }

            TempData["role"] = "student";
            GetLibCard(student.email);
            return Redirect("/Home");

        }
        public void GetLibCard(string email)
        {
            using (var conn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM students WHERE email='" + email + "'", conn))
                {
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        TempData["libraryCard"] = reader.GetInt32(0);
                        

                    }
                    else
                    {
                        TempData["libraryCard"] = -1;
                        

                    }
                }
            }
        }

        public IActionResult Login()
        {
            LoginStudentVM newStudent = new LoginStudentVM();

            return View(newStudent);
        }
        public IActionResult LoginLandingPage(LoginStudentVM newStudent, string returnUrl = "/Home")
        {
            Boolean studentFound=false;
            using (var conn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM students WHERE email='" + newStudent.email + "' AND password='" + newStudent.password + "'", conn))
                {
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        TempData["libraryCard"] = reader.GetInt32(0);
                        
                        TempData["role"] = "student".ToString();
                        studentFound = true;

                    }
                    else
                    {
                        TempData["libraryCard"] = -1;
                        TempData["role"] = "invalid";

                    }
                }
            }
                if(!studentFound)
                {
                    using (var subConn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))
                    {
                        subConn.Open();
                        using (var command = new NpgsqlCommand("SELECT * FROM employees WHERE email='" + newStudent.email + "' AND password = '" + newStudent.password + "'", subConn))
                        {
                            var reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                TempData["libraryCard"] = reader.GetInt32(0);
                                string role = "employee";
                                TempData["role"] = role;
                                studentFound = true;

                            }
                            else
                            {
                                TempData["libraryCard"] = -1;
                                TempData["role"] = "invalid";

                            }
                        }
                    }    
                    
                }
               
            
            return Redirect(returnUrl);

        }

        
        public IActionResult Logout()
        {
            string role = "guest";
            TempData["role"] = role;
            TempData["libraryCard"] = -1;

            return Redirect("/Home");
        }

        
        public IActionResult StudentProfile()
        {
            

            ViewStudentVM myStudent = new ViewStudentVM();
            int libCard = (int)TempData.Peek("libraryCard");
            using (var conn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM students WHERE library_card_number='" + libCard + "'", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        myStudent.library_card_number = reader.GetInt16(0);
                        myStudent.fname = reader.GetString(1);
                        myStudent.mname = reader.GetString(2);
                        myStudent.phonenumber = reader.GetString(7);
                        myStudent.overdueFees = reader.GetFloat(8);
                        myStudent.lname = reader.GetString(3);
                        myStudent.homeaddress = reader.GetString(4);
                        myStudent.age = reader.GetInt16(9);
                        myStudent.email=reader.GetString(5);
                        myStudent.password = reader.GetString(6);


                    }
                }

            }
            return View(myStudent);
        }


    }


}