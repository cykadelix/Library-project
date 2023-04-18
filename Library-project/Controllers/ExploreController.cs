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
using Library_project.ViewModels.Movie;
using Library_project.ViewModels.Audiobook;

using Microsoft.AspNetCore.Authorization;

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
            using (var conn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))
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
            using (var conn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))
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
            using (var conn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))
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
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));

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
                    isAvailable = reader.GetBoolean(7),
                    description = reader.GetString(9),
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

        //Journals
        [HttpGet]
        public List<CreateJournalViewModel>? JournalToList()
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));

            dataSourceBuilder.MapComposite<Location>();
            using var dataSource = dataSourceBuilder.Build();
            using var command = dataSource.CreateCommand("SELECT * FROM journals");
            using var reader = command.ExecuteReader();

            List<CreateJournalViewModel> local_list = new List<CreateJournalViewModel>();

            while (reader.Read())
            {
                local_list.Add(new CreateJournalViewModel()
                {
                    journalid = reader.GetInt32(0),
                    title = reader.GetFieldValue<string>(2),
                    researchers = reader.GetFieldValue<string>(3),
                    subject = reader.GetFieldValue<string>(4),
                    length = reader.GetFieldValue<int>(5),
                    releasedate = reader.GetFieldValue<DateOnly>(6).ToString("yyyy-MM-dd"),
                    isavailable = reader.GetFieldValue<bool>(7),
                    description = reader.GetString(8),
                }); ;
            }
            if (local_list.Count == 0)
            {
                return null;
            }
            return local_list;
        }

        [HttpGet]
        public PartialViewResult GetJournals()
        {
            return PartialView("~/Views/Explore/_JournalView.cshtml", JournalToList());
        }

        [HttpGet]
        public IActionResult GetJournalList()
        {
            return Json(JournalToList());
        }

        //Movies
        [HttpGet]
        public List<MovieViewModel>? MovieToList()
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));

            dataSourceBuilder.MapComposite<Location>();
            using var dataSource = dataSourceBuilder.Build();
            using var command = dataSource.CreateCommand("SELECT * FROM movies");
            using var reader = command.ExecuteReader();

            List<MovieViewModel> local_list = new List<MovieViewModel>();

            while (reader.Read())
            {
                local_list.Add(new MovieViewModel()
                {
                    movieid = reader.GetInt32(0),
                    rating = reader.GetFieldValue<int>(2),
                    title = reader.GetFieldValue<string>(3),
                    director = reader.GetFieldValue<string>(4),
                    genres = reader.GetFieldValue<int>(5),
                    length = reader.GetFieldValue<int>(6),
                    releasedate = reader.GetFieldValue<DateOnly>(7).ToString("yyyy-MM-dd"),
                    availability = reader.GetFieldValue<bool>(8),
                    description = reader.GetString(9),
                }); ;
            }
            if (local_list.Count == 0)
            {
                return null;
            }
            return local_list;
        }

        [HttpGet]
        public PartialViewResult GetMovies()
        {
            return PartialView("~/Views/Explore/_MovieView.cshtml", MovieToList());
        }

        [HttpGet]
        public IActionResult GetMovieList()
        {
            return Json(MovieToList());
        }

        //Audiobooks
        [HttpGet]
        public List<AudiobookViewModel>? AudiobookToList()
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));

            dataSourceBuilder.MapComposite<Location>();
            using var dataSource = dataSourceBuilder.Build();
            using var command = dataSource.CreateCommand("SELECT * FROM audiobooks");
            using var reader = command.ExecuteReader();

            List<AudiobookViewModel> local_list = new List<AudiobookViewModel>();

            while (reader.Read())
            {
                AudiobookViewModel audiobook = new AudiobookViewModel();
                audiobook.audiobookid = reader.GetInt32(0);
                audiobook.genre = reader.GetInt32(2);
                audiobook.title = reader.GetFieldValue<string>(3);
                audiobook.narrator = reader.GetFieldValue<string>(4);
                audiobook.author = reader.GetFieldValue<string>(5);
                audiobook.length = reader.GetFieldValue<TimeOnly>(6).ToString("hh:mm:ss");
                audiobook.availability = reader.GetFieldValue<bool>(7);
                audiobook.description = reader.GetString(8);
                local_list.Add(audiobook);
            }

            reader.Close();
            if (local_list.Count == 0)
            {
                return null;
            }
            return local_list;
        }

        [HttpGet]
        public PartialViewResult GetAudiobooks()
        {
            return PartialView("~/Views/Explore/_AudiobookView.cshtml", AudiobookToList());
        }

        [HttpGet]
        public IActionResult GetAudiobookList()
        {
            return Json(AudiobookToList());
        }
    }

}
