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

        [HttpGet]
        public IActionResult GetUserHoldList(int userid,string role)
        {
            return Json(userHoldsList(userid, role));
        }

        public IActionResult userHolds()
        {
            return View();
        }

        public List<CreateHoldVM> userHoldsList(int userid, string role)
        {
            
            List<CreateHoldVM> localList = new List<CreateHoldVM>();
            using NpgsqlConnection conn = new NpgsqlConnection(_config.GetConnectionString("local_lib"));
            conn.Open();
            string cmdString = "";
            if (role == "student")
            {
                 cmdString = "SELECT * FROM holds WHERE studentid="+ userid.ToString();
            }
            else
            {
                cmdString = "SELECT * FROM holds WHERE employeeid=" + userid.ToString();
            }
            
            using var cmd = new NpgsqlCommand(cmdString,conn);

            var reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                while (reader.Read())
                {
                    CreateHoldVM localHold = new CreateHoldVM();
                    if(TempData.Peek("role").ToString()=="student")
                    {
                        localHold.userid = reader.GetInt16(1);
                    }
                    else
                    {
                        localHold.userid=reader.GetInt32(4);
                    }
                    localHold.mediaid = reader.GetInt16(2);
                    localHold.date = reader.GetDateTime(3).ToString();
                    localHold.title = reader.GetString(5);
                    localList.Add(localHold);
                }
                
            }
                
            return localList;
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

                        commandOne = "INSERT INTO holds (holdid, studentid, mediaid, hold_date, employeeid, title) VALUES (DEFAULT, @userid, @mediaid, current_timestamp, -1, @title";
                    }
                    else
                    {
                        commandOne = "INSERT INTO holds (holdid, studentid, mediaid, hold_date, employeeid) VALUES (DEFAULT, -1, @mediaid, current_timestamp, @userid, @title)";
                    }
                    
                    using NpgsqlConnection conn = new NpgsqlConnection(_config.GetConnectionString("local_lib"));
                    conn.Open();

                    using var cmd = new NpgsqlCommand(commandOne, conn)
                    {
                        Parameters =
                            {
                                new("userid",newHold.userid),
                                new("mediaid",newHold.mediaid),
                                new("title", newHold.title)
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

