using Microsoft.AspNetCore.Mvc;
using Library_project.Models;
using Npgsql;
using Library_project.ViewModels;
using Library_project.Data.Enums;
using Library_project.Data.Objects;
using Library_project.ViewModels.Book;
using Library_project.ViewModels.Journal;
using Library_project.ViewModels.Camera;
using Library_project.ViewModels.Computer;
using Library_project.ViewModels.Projector;

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

        //Cameras
        public List<CameraViewModel>? CameraToList()
        {
            List<CameraViewModel> cameraList = new List<CameraViewModel>();
            using (var conn = new NpgsqlConnection(_config["ConnectionString"]))
            {
                conn.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM cameras", conn))
                {

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        CameraViewModel cam = new CameraViewModel();
                        cam.cameraid = reader.GetInt32(0);
                        cam.serialnumber = reader.GetString(1);
                        cam.brand = reader.GetString(2);
                        cam.description = reader.GetString(3);
                        cam.megapixels = reader.GetDouble(4);
                        cam.availability = reader.GetBoolean(5);
                        cameraList.Add(cam);
                    }
                    reader.Close();
                    if (cameraList.Count == 0)
                    {
                        return null;
                    }
                }
            }
            return cameraList;
        }

        [HttpGet]
        public PartialViewResult GetCameras()
        {
            return PartialView("~/Views/Explore/_CameraView.cshtml", CameraToList());
        }

        [HttpGet]
        public IActionResult GetCameraList()
        {
            return Json(CameraToList());
        }

        //Computers
        public List<ComputerViewModel>? ComputerToList()
        {
            List<ComputerViewModel> computerList = new List<ComputerViewModel>();
            using (var conn = new NpgsqlConnection(_config["ConnectionString"]))
            {
                conn.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM computers", conn))
                {

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ComputerViewModel comp = new ComputerViewModel();
                        comp.computerid = reader.GetInt32(0);
                        comp.serialnumber = reader.GetString(1);
                        comp.brand = reader.GetString(2);
                        comp.description = reader.GetString(3);
                        comp.availability = reader.GetBoolean(4);
                        computerList.Add(comp);
                    }
                    reader.Close();
                    if (computerList.Count == 0)
                    {
                        return null;
                    }
                }
            }
            return computerList;
        }

        [HttpGet]
        public PartialViewResult GetComputers()
        {
            return PartialView("~/Views/Explore/_ComputerView.cshtml", ComputerToList());
        }

        [HttpGet]
        public IActionResult GetComputerList()
        {
            return Json(ComputerToList());
        }

        //Projectors
        public List<ProjectorViewModel>? ProjectorToList()
        {
            List<ProjectorViewModel> computerList = new List<ProjectorViewModel>();
            using (var conn = new NpgsqlConnection(_config["ConnectionString"]))
            {
                conn.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM projectors", conn))
                {

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProjectorViewModel comp = new ProjectorViewModel();
                        comp.projectorid = reader.GetInt32(0);
                        comp.serialnumber = reader.GetString(1);
                        comp.brand = reader.GetString(2);
                        comp.description = reader.GetString(3);
                        comp.lumens = reader.GetInt32(4);
                        comp.availability = reader.GetBoolean(5);
                        computerList.Add(comp);
                    }
                    reader.Close();
                    if (computerList.Count == 0)
                    {
                        return null;
                    }
                }
            }
            return computerList;
        }

        [HttpGet]
        public PartialViewResult GetProjectors()
        {
            return PartialView("~/Views/Explore/_ProjectorView.cshtml", ProjectorToList());
        }

        [HttpGet]
        public IActionResult GetProjectorList()
        {
            return Json(ProjectorToList());
        }

        //Computer
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config["ConnectionString"]);
            dataSourceBuilder.MapEnum<genres>();
            dataSourceBuilder.MapComposite<Location>();
            await using var dataSource = dataSourceBuilder.Build();
            await using var command = dataSource.CreateCommand("SELECT * FROM book ,media WHERE bookId=media.mediaId");
            await using var reader = await command.ExecuteReaderAsync();

            var bookList = new BookListViewModel();
            var LocalList = new List<book>();
            while (await reader.ReadAsync())
            {
                LocalList.Add(new book()
                {
                    bookid = (int)reader["bookId"],
                    title = reader["title"] as string,
                    mediaid = (int)reader["bookId"],
                    isavailable = (bool)reader["isAvailable"],
                    isbn = (long)reader["isbn"],
                    pagecount = (int)reader["pageCount"],
                    publicdate = reader.GetFieldValue<DateOnly>(3),
                    author = reader.GetFieldValue<string[]>(2),
                    genres = reader.GetFieldValue<int>(8),
                    location = reader.GetFieldValue<Location>(10)
                });

                bookList.allBooks = LocalList;
            }
            return View(bookList);
        }


        [HttpPost]
        public async Task<IActionResult> CreateBookLandingPage(CreateBookViewModel newBook)
        {
            CreateBookViewModel example = new CreateBookViewModel();
            example.title = newBook.title;
            example.author = newBook.author;
            example.pageCount = newBook.pageCount;
            example.isbn = newBook.isbn;
            example.isAvailable = newBook.isAvailable;
            example.publishDate = newBook.publishDate;
            example.genre = newBook.genre;

            if (ModelState.IsValid)
            {
                await using NpgsqlConnection conn = new NpgsqlConnection(_config["ConnectionString"]);

                // Connect to the database
                await conn.OpenAsync();

                await using var command = new NpgsqlCommand("WITH local_id AS (INSERT INTO media VALUES (DEFAULT,(1,1)) RETURNING mediaid)" +
                    "INSERT INTO book " +
                    "(bookid," +
                    "title,author,publicdate,pagecount,isbn,isavailable,genre,mediaid)" +
                    " VALUES((SELECT mediaid from local_id) , @title, @author, @publicDate, " +
                    "@pageCount,@isbn,@isAvailable,@genre,(SELECT mediaid from local_id))", conn)
                {
                    Parameters =
                        {
                            new("title", newBook.title),
                            new("author", newBook.author),
                            new("genre", newBook.genre),
                            new("publicDate", newBook.publishDate),
                            new("pageCount", newBook.pageCount),
                            new("isbn", newBook.isbn),
                            new("isAvailable", newBook.isAvailable),
                        }
                };
                await using var reader = await command.ExecuteReaderAsync();
            }
            else
            {

                example.title = "fake";

            }
            return View(example);
        }
        public async Task<IActionResult> GetJournal()
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config["ConnectionString"]);

            dataSourceBuilder.MapComposite<Location>();
            await using var dataSource = dataSourceBuilder.Build();
            await using var command = dataSource.CreateCommand("SELECT * FROM journal ,media WHERE journalid=media.mediaId");
            await using var reader = await command.ExecuteReaderAsync();

            var journalList = new JournalListViewModel();
            List<journal> local_list = new List<journal>();

            while (await reader.ReadAsync())
            {
                local_list.Add(new journal()
                {
                    jouranalid = (int)reader["journalid"],
                    title = (string)reader["title"],
                    researchers = reader.GetFieldValue<string[]>(5),
                    subject = reader.GetFieldValue<string[]>(6),
                    length = (int)reader["length"],
                    releasedate = reader.GetFieldValue<DateOnly>(4)
                }); ;
            }
            journalList.allJournals = local_list;
            return View(journalList);

        }

    }

}
