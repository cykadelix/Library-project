using Library_project.Data.Enums;
using Library_project.Data.Objects;
using Library_project.Models;
using Library_project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Npgsql;

namespace Library_project.Controllers
{
    public class HistorianController : Controller
    {
        private readonly string connString = "Host=127.0.0.1;Server=localhost;Port=5432;Database=my_server;UserID=postgres;Password=Fuentes5;Pooling=true";



        public async Task<IActionResult> Index()
        {

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connString);
            await using var dataSource = dataSourceBuilder.Build();
            await using var command = dataSource.CreateCommand("SELECT * FROM historian");
            await using var reader = await command.ExecuteReaderAsync();

            var historianList = new listHistorianViewModel();
            var LocalList = new List<historian>();
            while (await reader.ReadAsync())
            {
                LocalList.Add(new historian()
                {
                    fname = (string)reader["fName"],
                    mname = (string)reader["mName"],
                    lname = (string)reader["lName"],
                    historianid = (int)reader["historianID"],
                    expertise = (string)reader["expertise"],
                    education = (string)reader["education"],
                    age = (short)reader["age"],
                    studentstosee = (List<student>)reader["studentsToSee"]
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
            example.fName = newHistorian.fName;
            example.mName = newHistorian.mName;
            example.lName = newHistorian.lName;
            example.historianID = newHistorian.historianID;
            example.expertise = newHistorian.expertise;
            example.education = newHistorian.education;
            example.age = newHistorian.age;
            example.studentsToSee = newHistorian.studentsToSee;

            if (ModelState.IsValid)
            {






                await using NpgsqlConnection conn = new NpgsqlConnection("Host=127.0.0.1;Server=localhost;Port=5432;Database=my_server;UserID=postgres;Password=Fuentes5;Pooling=true;Include Error Detail=true;");


                // Connect to the database
                await conn.OpenAsync();

                await using var command = new NpgsqlCommand("INSERT INTO historian(VALUES(" +
                    "@fName, @mName, @lName, DEFAULT, @expertise, @education, @age))", conn)
                {
                    Parameters =
                        {
                            new("fname", newHistorian.fname),
                            new("mname", newHistorian.mname),
                            new("lname", newHistorian.lname),
                            new("historianid", newHistorian.historianid),
                            new("expertise", newHistorian.expertise),
                            new("education", newHistorian.education),
                            new("age", newHistorian.age),
                        }
                };
                await using var reader = await command.ExecuteReaderAsync();








            }
            else
            {

                example.fName = "invalid";

            }
            return View(example);


        }





        public async Task<IActionResult> Edit(int historian_id)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connString);

            await using var dataSource = dataSourceBuilder.Build();
            await using var command = dataSource.CreateCommand("SELECT * FROM historian");
            await using var reader = await command.ExecuteReaderAsync();




            var localHistorian = new EditHistorianViewModel();
            reader.Read();
            using var innerRead = reader.GetData(0);

            while (innerRead.Read())
            {
                localHistorian.fName = innerRead.GetFieldValue<string>(1);
                localHistorian.mName = innerRead.GetFieldValue<string>(2);
                localHistorian.lName = innerRead.GetFieldValue<string>(3);
                localHistorian.historianID = innerRead.GetFieldValue<int>(4);
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
