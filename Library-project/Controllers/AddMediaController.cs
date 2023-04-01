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

                    using (var command = new NpgsqlCommand("INSERT INTO camera (brand, serialnumber, description, lumens, availability) VALUES (@b1, @s1, @d1, @l1, @a1)", conn))
                    {
                        command.Parameters.AddWithValue("b1", model.brand);
                        command.Parameters.AddWithValue("s1", model.serialnumber);
                        if (model.description != null)
                            command.Parameters.AddWithValue("d1", model.description);
                        else
                            command.Parameters.AddWithValue("d1", "No description provided");
                        command.Parameters.AddWithValue("l1", model.lumens);
                        command.Parameters.AddWithValue("a1", model.availability);

                        int nRows = command.ExecuteNonQuery();
                    }
                }
            }
            ModelState.Clear();
            return View();
        }

        public IActionResult AddComputer()
        {
            return View();
        }
    }

    
}
