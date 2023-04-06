using Library_project.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Library_project.Controllers
{
    public class AddMediaController : Controller
    {
        private readonly IConfiguration _config;

        public AddMediaController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult AddCamera()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCamera(camera model)
        {
            if (model.brand == null)
            {
                return View();
            }

            if (ModelState.IsValid)
            {
                using (var conn = new NpgsqlConnection(_config["ConnectionString"]))

                {
                    conn.Open();
                    var insertCommand = "WITH newid AS (INSERT INTO media (mediaid) VALUES (default) RETURNING mediaid)" +
                        "INSERT INTO camera (brand, serialnumber, description, lumens, availability)";
                    using (var command = new NpgsqlCommand(insertCommand + " VALUES (@b1, (SELECT mediaid from newid), @d1, @l1, @a1)", conn))
                    {
                        command.Parameters.AddWithValue("b1", model.brand);
                        if (model.description != null)
                            command.Parameters.AddWithValue("d1", model.description);
                        else
                            command.Parameters.AddWithValue("d1", "No description provided");
                        command.Parameters.AddWithValue("l1", model.lumens);
                        if (model.availability == null)
                            command.Parameters.AddWithValue("a1", false);
                        else
                            command.Parameters.AddWithValue("a1", true);

                        int nRows = command.ExecuteNonQuery();
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditCameraForm(int serialNo)
        {
            camera cam = new camera();
            using (var conn = new NpgsqlConnection(_config["ConnectionString"]))
            {

                Console.Out.WriteLine("Opening connection");
                conn.Open();

                var sqlCommand = "SELECT * FROM camera WHERE serialnumber='" + serialNo.ToString() + "'";
                using (var command = new NpgsqlCommand(sqlCommand, conn))
                {

                    var reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        cam.brand = reader.GetString(0);
                        cam.serialnumber = reader.GetInt32(1);
                        cam.description = reader.GetString(2);
                        cam.lumens = reader.GetInt32(3);
                        cam.availability = reader.GetBoolean(4);
                    }
                    reader.Close();
                }
            }
            return View("~/Views/AddMedia/AddCamera.cshtml", cam);
        }

        [HttpPost]
        public IActionResult UpdateCamera(camera model)
        {
            if (ModelState.IsValid)
            {
                using (var conn = new NpgsqlConnection(_config["ConnectionString"]))

                {
                    conn.Open();
                    var updateCommand = "UPDATE camera SET brand=@b1, description=@d1, lumens=@l1, availability=@a1 " + 
                        "WHERE serialnumber='" + model.serialnumber + "'";
                    using (var command = new NpgsqlCommand(updateCommand, conn))
                    {
                        command.Parameters.AddWithValue("b1", model.brand);
                        if (model.description != null)
                            command.Parameters.AddWithValue("d1", model.description);
                        else
                            command.Parameters.AddWithValue("d1", "No description provided");
                        command.Parameters.AddWithValue("l1", model.lumens);
                        if (model.availability == null)
                            command.Parameters.AddWithValue("a1", false);
                        else
                            command.Parameters.AddWithValue("a1", true);

                        int nRows = command.ExecuteNonQuery();
                        Console.Write(nRows);
                    }
                }
            }
            return View("~/Views/AddMedia/AddCamera.cshtml");
        }

        [HttpDelete]
        public IActionResult DeleteCamera(int serialNo)
        {
            camera cam = new camera();
            using (var conn = new NpgsqlConnection(_config["ConnectionString"]))
            {

                Console.Out.WriteLine("Opening connection");
                conn.Open();

                var sqlCommand = "DELETE FROM camera WHERE serialnumber='" + serialNo.ToString() + "'";
                using (var command = new NpgsqlCommand(sqlCommand, conn))
                {
                    int nRows = command.ExecuteNonQuery();
                }
            }
            return new JsonResult(new { redirectToUrl = Url.Action("AddCamera", "AddMedia") });
        }

        public IActionResult AddComputer()
        {
            return View();
        }

    }


}
