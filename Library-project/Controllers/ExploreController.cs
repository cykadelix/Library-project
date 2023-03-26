using Microsoft.AspNetCore.Mvc;
using Library_project.Models;
using Npgsql;
using Library_project.ViewModels;

namespace Library_project.Controllers
{
    public class ExploreController : Controller
    {
        private readonly IConfiguration _config;

        public ExploreController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult GetCameras()
        {
            ListCameraViewModel cameraList = new ListCameraViewModel();
            using (var conn = new NpgsqlConnection(_config["ConnectionString"]))
            {

                Console.Out.WriteLine("Opening connection");
                conn.Open();


                using (var command = new NpgsqlCommand("SELECT * FROM camera", conn))
                {

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        camera cam = new camera();
                        cam.brand = reader.GetString(0);
                        cam.serialnumber = reader.GetInt32(1);
                        cam.description = reader.GetString(2);
                        cam.lumens = reader.GetInt32(3);
                        cam.availibility = reader.GetBoolean(4);
                        cameraList.Cameras.Add(cam);
                    }
                    reader.Close();
                }
            }
            return PartialView("~/Views/Explore/_CameraView.cshtml", cameraList);
        }
        [HttpPost]
        public IActionResult AddCamera(camera camera)
        {

            return RedirectToAction("AddMediaForm", "Home");
        }
    }
}
