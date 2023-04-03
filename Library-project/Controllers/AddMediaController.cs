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
            if(model.brand == null)
            {
                return View();
            }

            if(ModelState.IsValid)
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
                        if(model.availability == null)
                            command.Parameters.AddWithValue("a1", false);
                        else
                            command.Parameters.AddWithValue("a1", true);

                        int nRows = command.ExecuteNonQuery();
                    }
                }
            }
            return View();
        }

        public IActionResult AddComputer()
        {
            return View();
        }
    }

    
}
