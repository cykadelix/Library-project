using Library_project.Data.Enums;
using Library_project.Data.Objects;
using Library_project.Models;
using Library_project.ViewModels.Historian;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Npgsql;

namespace Library_project.Controllers
{
    public class HistorianController : Controller
    {
        private readonly IConfiguration _config;
        public HistorianController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult HistorianIndex()
        {
            return View();
        }

        public  List<CreateHistorianViewModel>? HistorianToList()
        {

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));
            using var dataSource = dataSourceBuilder.Build();
            using var command = dataSource.CreateCommand("SELECT * FROM historians");
            using var reader = command.ExecuteReader();

            var LocalList = new List<CreateHistorianViewModel>();
            while (reader.Read())
            {
                LocalList.Add(new CreateHistorianViewModel()
                {
                    fname = (string)reader["fname"],
                    mname = (string)reader["mname"],
                    lname = (string)reader["lname"],
                    historianid = (int)reader["historianid"],
                    expertise = (string)reader["expertise"],
                    education = (string)reader["education"],
                    age = (short)reader["age"],
                    active = (bool)reader["active"],
                });
            }
            if(LocalList.Count == 0)
            {
                return null;
            }
            return LocalList;

        }

        [HttpGet]
        public IActionResult GetHistorianList()
        {
            return Json(HistorianToList());
        }

        [HttpPost]
        public IActionResult CreateHistorian(CreateHistorianViewModel model)
        {
            if(model.fname ==  null)
            {
                return View("~/Views/Historian/HistorianIndex.cshtml");
            }
            else if (ModelState.IsValid)
            {
                using NpgsqlConnection conn = new NpgsqlConnection(_config.GetConnectionString("local_lib"));
                // Connect to the database
                conn.Open();

                using var command = new NpgsqlCommand("INSERT INTO historians (VALUES(" +
                    "DEFAULT, @fname, @mname, @lname, @expertise, @education, @age, @active))", conn)
                {
                    Parameters =
                        {
                            new("fname", model.fname),
                            new("mname", (model.mname == null? "": model.mname)),
                            new("lname", model.lname),
                            new("expertise", model.expertise),
                            new("education", model.education),
                            new("age", model.age),
                            new("active", model.active)
                        }
                };
                using var reader = command.ExecuteReader();
            }
            return View("~/Views/Historian/HistorianIndex.cshtml");
        }

        [HttpGet]
        public IActionResult EditHistorian(int historianId)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));

            using var dataSource = dataSourceBuilder.Build();
            using var command = dataSource.CreateCommand("SELECT * FROM historians where historianid='" + historianId + "'");
            using var reader = command.ExecuteReader();

            var localHistorian = new CreateHistorianViewModel();
            while (reader.Read())
            {
                localHistorian.historianid = reader.GetFieldValue<int>(0);
                localHistorian.fname = reader.GetFieldValue<string>(1);
                localHistorian.mname = reader.GetFieldValue<string>(2);
                localHistorian.lname = reader.GetFieldValue<string>(3);
                localHistorian.expertise = reader.GetFieldValue<string>(4);
                localHistorian.education = reader.GetFieldValue<string>(5);
                localHistorian.age = reader.GetFieldValue<short>(6);
                localHistorian.active = reader.GetFieldValue<bool>(7);
            }

            return View("~/Views/Historian/HistorianIndex.cshtml", localHistorian);
        }

        [HttpPost]
        public IActionResult UpdateHistorian(CreateHistorianViewModel model)
        {
            using (var conn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))
            {
                conn.Open();
                string queryParameters = "fname=@f1, mname=@m1, lname=@l1, expertise=@e1, education=@e2, age=@a1, active=@a2 ";
                string updateCommand = "UPDATE historians SET " + queryParameters + "WHERE historianid='" + model.historianid + "'";
                using (var command = new NpgsqlCommand(updateCommand, conn))
                {
                    command.Parameters.AddWithValue("f1", model.fname);
                    command.Parameters.AddWithValue("m1", (model.mname == null ? "" : model.mname));
                    command.Parameters.AddWithValue("l1", model.lname);
                    command.Parameters.AddWithValue("e1", model.expertise);
                    command.Parameters.AddWithValue("e2", model.education);
                    command.Parameters.AddWithValue("a1", model.age);
                    command.Parameters.AddWithValue("a2", model.active);

                    int nRows = command.ExecuteNonQuery();

                }
            }

            return View("~/Views/Historian/HistorianIndex.cshtml");
        }

        [HttpDelete]
        public IActionResult DeleteHistorian(int historianId)
        {
            using (var conn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))
            {
                conn.Open();

                var sqlCommand = "UPDATE historians SET active='false' WHERE historianid='" + historianId.ToString() + "';";
                using (var command = new NpgsqlCommand(sqlCommand, conn))
                {
                    int nRows = command.ExecuteNonQuery();
                }
            }
            return new JsonResult(new { redirectToUrl = Url.Action("HistorianIndex", "Historian") });
        }
    }
}
