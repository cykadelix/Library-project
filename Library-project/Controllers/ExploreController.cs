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

        //Books
        [HttpGet]
        public List<EditBookViewModel>? BookToList()
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config["ConnectionString"]);
            dataSourceBuilder.MapEnum<genres>();
            dataSourceBuilder.MapComposite<Location>();
            using var dataSource = dataSourceBuilder.Build();
            using var command = dataSource.CreateCommand("SELECT * FROM books");
            using var reader = command.ExecuteReader();

            var bookList = new List<EditBookViewModel>();
            while (reader.Read())
            {
                bookList.Add(new EditBookViewModel()
                {
                    bookid = reader.GetInt32(0),
                    title = reader.GetString(1),
                    author = reader.GetString(2),
                    genres = reader.GetInt32(3),
                    publicDate = reader.GetFieldValue<DateOnly>(4).ToString(),
                    pageCount = reader.GetInt32(5),
                    isbn = reader.GetInt64(6),
                    isAvailable = reader.GetBoolean(7)
                });
            }
            if (bookList.Count == 0)
            {
                return null;
            }
            return bookList;
        }

        [HttpGet]
        public PartialViewResult GetBooks()
        {
            return PartialView("~/Views/Explore/_BookView.cshtml", BookToList());
        }

        [HttpGet]
        public IActionResult GetBookList()
        {
            return Json(BookToList());
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
                    researchers = reader.GetFieldValue<string>(5),
                    subject = reader.GetFieldValue<string>(6),
                    length = (int)reader["length"],
                    releasedate = reader.GetFieldValue<DateOnly>(4)
                }); ;
            }
            journalList.allJournals = local_list;
            return View(journalList);

        }

    }

}
