using Library_project.ViewModels.Camera;
using Library_project.ViewModels.Computer;
using Library_project.ViewModels.Projector;
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

        //Cameras
        public IActionResult AddCamera()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCamera(CameraViewModel model)
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
                    var insertCommand = "WITH newid AS (INSERT INTO medias (mediaid) VALUES (default) RETURNING mediaid)" +
                        "INSERT INTO cameras (cameraid, serialnumber, brand, description, megapixels, availability, mediaid)";
                    using (var command = new NpgsqlCommand(insertCommand + " VALUES ((SELECT mediaid from newid), @s1, @b1, @d1, @m1, @a1, (SELECT mediaid from newid))", conn))
                    {
                        command.Parameters.AddWithValue("s1", model.serialnumber);
                        command.Parameters.AddWithValue("b1", model.brand);
                        if (model.description != null)
                            command.Parameters.AddWithValue("d1", model.description);
                        else
                            command.Parameters.AddWithValue("d1", "No description provided");
                        command.Parameters.AddWithValue("m1", model.megapixels);
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
        public IActionResult EditCameraForm(int cameraId)
        {
            CameraViewModel cam = new CameraViewModel();
            using (var conn = new NpgsqlConnection(_config["ConnectionString"]))
            {

                Console.Out.WriteLine("Opening connection");
                conn.Open();

                var sqlCommand = "SELECT * FROM cameras WHERE cameraid='" + cameraId.ToString() + "'";
                using (var command = new NpgsqlCommand(sqlCommand, conn))
                {

                    var reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        cam.cameraid = reader.GetInt32(0);
                        cam.serialnumber = reader.GetString(1);
                        cam.brand = reader.GetString(2);
                        cam.description = reader.GetString(3);
                        cam.megapixels = reader.GetDouble(4);
                        cam.availability = reader.GetBoolean(5);
                    }
                    reader.Close();
                }
            }
            return View("~/Views/AddMedia/AddCamera.cshtml", cam);
        }

        [HttpPost]
        public IActionResult UpdateCamera(CameraViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var conn = new NpgsqlConnection(_config["ConnectionString"]))

                {
                    conn.Open();
                    var updateCommand = "UPDATE cameras SET serialnumber=@s1, brand=@b1, description=@d1, megapixels=@l1, availability=@a1 " + 
                        "WHERE cameraid='" + model.cameraid + "'";
                    using (var command = new NpgsqlCommand(updateCommand, conn))
                    {
                        command.Parameters.AddWithValue("s1", model.serialnumber);
                        command.Parameters.AddWithValue("b1", model.brand);
                        if (model.description != null)
                            command.Parameters.AddWithValue("d1", model.description);
                        else
                            command.Parameters.AddWithValue("d1", "No description provided");
                        command.Parameters.AddWithValue("l1", model.megapixels);
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
        public IActionResult DeleteCamera(int cameraId)
        {
            using (var conn = new NpgsqlConnection(_config["ConnectionString"]))
            {

                Console.Out.WriteLine("Opening connection");
                conn.Open();

                var sqlCommand = "DELETE FROM cameras WHERE cameraId='" + cameraId.ToString() + "';";
                sqlCommand += "DELETE FROM medias WHERE mediaid='" + cameraId.ToString() + "'";
                using (var command = new NpgsqlCommand(sqlCommand, conn))
                {
                    int nRows = command.ExecuteNonQuery();
                }
            }
            return new JsonResult(new { redirectToUrl = Url.Action("AddCamera", "AddMedia") });
        }
        
        //Computers
        public IActionResult AddComputer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddComputer(ComputerViewModel model)
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
                    var insertCommand = "WITH newid AS (INSERT INTO medias (mediaid) VALUES (default) RETURNING mediaid)" +
                        "INSERT INTO computers (computerid, serialnumber, brand, description, availability, mediaid)";
                    using (var command = new NpgsqlCommand(insertCommand + " VALUES ((SELECT mediaid from newid), @s1, @b1, @d1, @a1, (SELECT mediaid from newid))", conn))
                    {
                        command.Parameters.AddWithValue("s1", model.serialnumber);
                        command.Parameters.AddWithValue("b1", model.brand);
                        if (model.description != null)
                            command.Parameters.AddWithValue("d1", model.description);
                        else
                            command.Parameters.AddWithValue("d1", "No description provided");
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
        public IActionResult EditComputerForm(int computerId)
        {
            ComputerViewModel comp = new ComputerViewModel();
            using (var conn = new NpgsqlConnection(_config["ConnectionString"]))
            {

                Console.Out.WriteLine("Opening connection");
                conn.Open();

                var sqlCommand = "SELECT * FROM computers WHERE computerid='" + computerId.ToString() + "'";
                using (var command = new NpgsqlCommand(sqlCommand, conn))
                {

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        comp.computerid = reader.GetInt32(0);
                        comp.serialnumber = reader.GetString(1);
                        comp.brand = reader.GetString(2);
                        comp.description = reader.GetString(3);
                        comp.availability = reader.GetBoolean(4);
                    }
                    reader.Close();
                }
            }
            return View("~/Views/AddMedia/AddComputer.cshtml", comp);
        }

        [HttpDelete]
        public IActionResult DeleteComputer(int computerId)
        {
            using (var conn = new NpgsqlConnection(_config["ConnectionString"]))
            {

                Console.Out.WriteLine("Opening connection");
                conn.Open();

                var sqlCommand = "DELETE FROM computers WHERE cameraId='" + computerId.ToString() + "';";
                sqlCommand += "DELETE FROM medias WHERE mediaid='" + computerId.ToString() + "'";
                using (var command = new NpgsqlCommand(sqlCommand, conn))
                {
                    int nRows = command.ExecuteNonQuery();
                }
            }
            return new JsonResult(new { redirectToUrl = Url.Action("AddComputer", "AddMedia") });
        }

        //Projectors
        public IActionResult AddProjector()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProjector(ProjectorViewModel model)
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
                    var insertCommand = "WITH newid AS (INSERT INTO medias (mediaid) VALUES (default) RETURNING mediaid)" +
                        "INSERT INTO projectors (projectorid, serialnumber, brand, description, lumens, availability, mediaid)";
                    using (var command = new NpgsqlCommand(insertCommand + " VALUES ((SELECT mediaid from newid), @s1, @b1, @d1, @l1, @a1, (SELECT mediaid from newid))", conn))
                    {
                        command.Parameters.AddWithValue("s1", model.serialnumber);
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
        public IActionResult EditProjectorForm(int projectorId)
        {
            ProjectorViewModel proj = new ProjectorViewModel();
            using (var conn = new NpgsqlConnection(_config["ConnectionString"]))
            {

                Console.Out.WriteLine("Opening connection");
                conn.Open();

                var sqlCommand = "SELECT * FROM projectors WHERE projectorid='" + projectorId.ToString() + "'";
                using (var command = new NpgsqlCommand(sqlCommand, conn))
                {

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        proj.projectorid = reader.GetInt32(0);
                        proj.serialnumber = reader.GetString(1);
                        proj.brand = reader.GetString(2);
                        proj.description = reader.GetString(3);
                        proj.lumens = reader.GetInt32(4);
                        proj.availability = reader.GetBoolean(5);
                    }
                    reader.Close();
                }
            }
            return View("~/Views/AddMedia/AddComputer.cshtml", proj);
        }

        [HttpDelete]
        public IActionResult DeleteProjector(int projectorId)
        {
            using (var conn = new NpgsqlConnection(_config["ConnectionString"]))
            {

                Console.Out.WriteLine("Opening connection");
                conn.Open();

                var sqlCommand = "DELETE FROM projectors WHERE cameraId='" + projectorId.ToString() + "';";
                sqlCommand += "DELETE FROM medias WHERE mediaid='" + projectorId.ToString() + "'";
                using (var command = new NpgsqlCommand(sqlCommand, conn))
                {
                    int nRows = command.ExecuteNonQuery();
                }
            }
            return new JsonResult(new { redirectToUrl = Url.Action("AddProjector", "AddMedia") });
        }
    }
}
