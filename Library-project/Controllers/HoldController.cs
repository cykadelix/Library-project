using Library_project.ViewModels.Hold;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Library_project.Controllers
{
    public class HoldController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public HoldController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<string> HoldItem(CreateHoldVM newHold)
        {

            if(ModelState.IsValid)
            {
                try
                {
                    
                    string commandOne = "";
                    string commandTwo= "UPDATE medias SET onhold=true WHERE mediaid="+newHold.mediaid+"";
                    
                    if (TempData.Peek("role").ToString() == "student")
                    {

                        commandOne = "INSERT INTO holds (holdid, studentid, mediaid, hold_date, employeeid) VALUES (DEFAULT, @userid, @mediaid, current_timestamp, -1) ";
                    }
                    else
                    {
                        commandOne = "INSERT INTO holds (holdid, studentid, mediaid, hold_date, employeeid) VALUES (DEFAULT, -1, @mediaid, current_timestamp, @userid) ";
                    }
                    
                    using NpgsqlConnection conn = new NpgsqlConnection(_config.GetConnectionString("local_lib"));
                    conn.Open();

                    using var cmd = new NpgsqlCommand(commandOne, conn)
                    {
                        Parameters =
                            {
                                new("userid",newHold.userid),
                                new("mediaid",newHold.mediaid)
                            }
                    };
                    cmd.ExecuteNonQuery();
                    using var cmd2 = new NpgsqlCommand(commandTwo, conn);
                    cmd2.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    return e.Message;
                }
               
                
                
            }
            return "";
        }

    }
}

