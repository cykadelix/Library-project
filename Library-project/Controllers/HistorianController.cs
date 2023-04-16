using Library_project.Data.Enums;
using Library_project.Data.Objects;
using Library_project.Models;
using Library_project.ViewModels.Historian;
using Microsoft.AspNetCore.Mvc;
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


        public async Task<IActionResult> HistorianIndex()
        {

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));
            await using var dataSource = dataSourceBuilder.Build();
            await using var command = dataSource.CreateCommand("SELECT * FROM historians");
            await using var reader = await command.ExecuteReaderAsync();

            var historianList = new listHistorianViewModel();
            var LocalList = new List<historians>();
            while (await reader.ReadAsync())
            {
                LocalList.Add(new historians()
                {
                    fname = (string)reader["fname"],
                    mname = (string)reader["mname"],
                    lname = (string)reader["lname"],
                    historianid = (int)reader["historianid"],
                    expertise = (string)reader["expertise"],
                    education = (string)reader["education"],
                    age = (short)reader["age"],
                    //studentstosee = (List<student>)reader["studentsToSee"]
                });

                historianList.allHistorians = LocalList;
            }


            return View(historianList);

        }
        public IActionResult historianForm()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateHistorianView()
        {
            var newHistorian = new CreateHistorianViewModel();

            return View(newHistorian);
        }
        [HttpPost]
        public async Task<IActionResult> CreateHistorianLandingPage(CreateHistorianViewModel newHistorian)
        {
            CreateHistorianViewModel example = new CreateHistorianViewModel();
            example.fname = newHistorian.fname;
            example.mname = newHistorian.mname;
            example.lname = newHistorian.lname;
            example.expertise = newHistorian.expertise;
            example.education = newHistorian.education;
            example.age = newHistorian.age;

            if (ModelState.IsValid)
            {
                await using NpgsqlConnection conn = new NpgsqlConnection(_config.GetConnectionString("local_lib"));
                // Connect to the database
                await conn.OpenAsync();

                await using var command = new NpgsqlCommand("INSERT INTO historians (VALUES(" +
                    "DEFAULT, @fname, @mname, @lname, @expertise, @education, @age))", conn)
                {
                    Parameters =
                        {
                            new("fname", newHistorian.fname),
                            new("mname", newHistorian.mname),
                            new("lname", newHistorian.lname),
                            new("expertise", newHistorian.expertise),
                            new("education", newHistorian.education),
                            new("age", newHistorian.age)
                        }
                };
                await using var reader = await command.ExecuteReaderAsync();
            }
            else
            {

                example.fname = "invalid";

            }
            return View(example);
        }

        public async Task<IActionResult> Edit(int historian_id)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));

            await using var dataSource = dataSourceBuilder.Build();
            await using var command = dataSource.CreateCommand("SELECT * FROM historian");
            await using var reader = await command.ExecuteReaderAsync();

            var localHistorian = new EditHistorianViewModel();
            reader.Read();
            using var innerRead = reader.GetData(0);

            while (innerRead.Read())
            {
                localHistorian.fname = innerRead.GetFieldValue<string>(1);
                localHistorian.mname = innerRead.GetFieldValue<string>(2);
                localHistorian.lname = innerRead.GetFieldValue<string>(3);
                localHistorian.expertise = innerRead.GetFieldValue<string>(5);
                localHistorian.education = innerRead.GetFieldValue<string>(6);
                localHistorian.age = innerRead.GetFieldValue<short>(7);
            }

            return View(localHistorian);
        }
        public async Task<IActionResult> Edit(EditHistorianViewModel editHistorianvm)
        {

            return RedirectToAction("Index");
        }
    }
}
