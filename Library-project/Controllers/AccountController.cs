using Library_project.Models;
using Library_project.ViewModels.Student;
using Npgsql;
using System.Security.Claims;

namespace Library_project.Controllers
{
    public class AccountController: Controller
    {
        IConfiguration _config;
        public AccountController(IConfiguration config)
        {
            _config = config; 
        }
        public IActionResult StudentRegistration()
        {
            RegisterStudentVM newStudent=new RegisterStudentVM();
            return View(newStudent);
        }

        public IActionResult RegisterStudentLandingPage(RegisterStudentVM student)
        {
            if(ModelState.IsValid)
            {
                using (var conn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))
                {
                    conn.Open();
                    var insertCommand = "INSERT INTO elijah_student (library_card_number, fname, mname, lname, homeaddress, birthday, email, phonenumber, historianshistorianid, overduefees)";
                    using (var command = new NpgsqlCommand(insertCommand + " VALUES(@library_card_number, @fname, @mname, @lname, @homeaddress, @birthday, @email, @phonenumber,0 ,0)", conn))
                    { 
                        command.Parameters.AddWithValue("@library_card_number", "Incomplete");
                        command.Parameters.AddWithValue("fname", student.fname);
                        command.Parameters.AddWithValue("mname", student.mname);
                        command.Parameters.AddWithValue("lname", student.lname);
                        command.Parameters.AddWithValue("homeaddress",      student.homeaddress);
                        command.Parameters.AddWithValue("birthday", student.birthday);
                        command.Parameters.AddWithValue("email", student.email);
                        command.Parameters.AddWithValue("phonenumber", student.email);

                        command.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                student.fname = "fake name";
            }


            return Redirect("Login");

        }
        public async Task Login(RegisterStudentVM newStudent, string returnUrl = "Account/PostLoginCheck")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .Build();

            

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            
        }

        public IActionResult PostLoginCheck()
        {
            return View();
        }
        public void CompleteRegistration()
        {
            string id="";
            string localEmail = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
            using (var conn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM elijah_student WHERE email='"+ localEmail + "'", conn))
                {
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        id=reader.GetString(0);
                    }
                    else
                    {
                        id = "yea right";
                    }
                }

            }
            if(id=="Incomplete")
            {
                using (var subConn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))
                {
                    subConn.Open();
                    var updateCommand = "UPDATE elijah_student SET library_card_number=@lc WHERE email=@email";
                    using (var subCommand = new NpgsqlCommand(updateCommand, subConn))
                    {
                        subCommand.Parameters.AddWithValue("lc", User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
                        subCommand.Parameters.AddWithValue("email", User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value);
                        subCommand.ExecuteNonQuery();
                    }
                }
            }
           
            Redirect("Home/Index");
            

        }
        public void updateLibraryCard(string card)
        {
            using(var conn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))
            {
                conn.Open();
                var updateCommand = "UPDATE elijah_student SET library_card_number=@lc WHERE email=@email";
                using(var command = new NpgsqlCommand(updateCommand,conn))
                {
                    command.Parameters.AddWithValue("lc", card);
                    command.Parameters.AddWithValue("emal", User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        [Authorize]
        public async Task Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action("Index", "Home"))
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        public IActionResult UserProfile()
        {
            string localEmail = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

            ViewStudentVM myStudent = new ViewStudentVM();  

            using (var conn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM elijah_student WHERE email='" + localEmail + "'", conn))
                {
                    var reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        myStudent.library_card_number = reader.GetString(0);
                        myStudent.fname = reader.GetString(1);
                        myStudent.mname = reader.GetString(2);
                        myStudent.phonenumber = reader.GetString(4);
                        if(reader.GetInt16(5)!=null)
                        {
                            myStudent.historianid = reader.GetInt16(5);
                        }
                        myStudent.overdueFees = reader.GetFloat(6);
                        myStudent.lname = reader.GetString(7);
                        myStudent.homeaddress = reader.GetString(8);
                        myStudent.birthday = reader.GetFieldValue<DateOnly>(9).ToString("yyyy-MM-dd");
                        myStudent.email = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                    }
                }

            }
            return View(myStudent);
        }


    }
        

}

