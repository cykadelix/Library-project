using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Library_project.Data.Enums;
using Library_project.Data.Objects;
using Library_project.ViewModels.Book;
using Library_project.ViewModels.Journal;
using Library_project.ViewModels.Camera;
using Library_project.ViewModels.Computer;
using Library_project.ViewModels.Projector;
using Library_project.ViewModels.Movie;
using Library_project.ViewModels.Audiobook;

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
        public CameraListViewModel? CameraToList()
        {
            CameraListViewModel cameraListViewModel = new CameraListViewModel();
            List<CameraViewModel> cameraList = new List<CameraViewModel>();
            int numAvailable = 0;
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
                        if (cam.availability == true) numAvailable++;
                    }
                    reader.Close();
                    if (cameraList.Count == 0)
                    {
                        return null;
                    }
                }
            }
            cameraListViewModel.Cameras = cameraList;
            cameraListViewModel.NumAvailable = numAvailable;
            return cameraListViewModel;
        }

        [HttpGet]
        public PartialViewResult GetCameras()
        {
            return PartialView("~/Views/Explore/_CameraView.cshtml", CameraToList());
        }

        [HttpGet]
        public IActionResult GetCameraList()
        {
            return Json(CameraToList() is null ? null : CameraToList().Cameras);
        }

        //Computers
        public ComputerListViewModel? ComputerToList()
        {
            ComputerListViewModel computerListViewModel = new ComputerListViewModel();
            List<ComputerViewModel> computerList = new List<ComputerViewModel>();
            int numAvailable = 0;
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
                        if (comp.availability == true) numAvailable++;
                    }
                    reader.Close();
                    if (computerList.Count == 0)
                    {
                        return null;
                    }
                }
            }
            computerListViewModel.Computers = computerList;
            computerListViewModel.NumAvailable = numAvailable;
            return computerListViewModel;
        }

        [HttpGet]
        public PartialViewResult GetComputers()
        {
            return PartialView("~/Views/Explore/_ComputerView.cshtml", ComputerToList());
        }

        [HttpGet]
        public IActionResult GetComputerList()
        {
            return Json(ComputerToList() is null ? null : ComputerToList().Computers);
        }

        //Projectors
        public ProjectorListViewModel? ProjectorToList()
        {
            ProjectorListViewModel projectorListViewModel = new ProjectorListViewModel();
            List<ProjectorViewModel> projectorList = new List<ProjectorViewModel>();
            int numAvailable = 0;
            using (var conn = new NpgsqlConnection(_config.GetConnectionString("local_lib")))
            {
                conn.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM projectors", conn))
                {

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProjectorViewModel proj = new ProjectorViewModel();
                        proj.projectorid = reader.GetInt32(0);
                        proj.serialnumber = reader.GetString(1);
                        proj.brand = reader.GetString(2);
                        proj.description = reader.GetString(3);
                        proj.lumens = reader.GetInt32(4);
                        proj.availability = reader.GetBoolean(5);
                        projectorList.Add(proj);
                        if (proj.availability == true) numAvailable++;
                    }
                    reader.Close();
                    if (projectorList.Count == 0)
                    {
                        return null;
                    }
                }
            }
            projectorListViewModel.Projectors = projectorList;
            projectorListViewModel.NumAvailable = numAvailable;
            return projectorListViewModel;
        }

        [HttpGet]
        public PartialViewResult GetProjectors()
        {
            return PartialView("~/Views/Explore/_ProjectorView.cshtml", ProjectorToList());
        }

        [HttpGet]
        public IActionResult GetProjectorList()
        {
            return Json(ProjectorToList() is null ? null : ProjectorToList().Projectors);
        }


        //Books
        [HttpGet]
        public BookListViewModel? BookToList()
        {
            var bookListViewModel = new BookListViewModel();
            var numAvailable = 0;
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));

            dataSourceBuilder.MapEnum<genres>();
            dataSourceBuilder.MapComposite<Location>();
            using var dataSource = dataSourceBuilder.Build();
            using var command = dataSource.CreateCommand("SELECT * FROM books");
            using var reader = command.ExecuteReader();

            var bookList = new List<BookViewModel>();
            while (reader.Read())
            {
                bookList.Add(new BookViewModel()
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
                if (reader.GetBoolean(7) == true) numAvailable++;
            }
            if (bookList.Count == 0)
            {
                return null;
            }
            bookListViewModel.Books = bookList;
            bookListViewModel.NumAvailable = numAvailable;
            return bookListViewModel;
        }

        [HttpGet]
        public PartialViewResult GetBooks()
        {
            return PartialView("~/Views/Explore/_BookView.cshtml", BookToList());
        }

        [HttpGet]
        public IActionResult GetBookList()
        {
            return Json(BookToList() is null ? null : BookToList().Books);
        }

        //Journals
        [HttpGet]
        public JournalListViewModel? JournalToList()
        {
            var journalListViewModel = new JournalListViewModel();
            var numAvailable = 0;
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("local_lib"));

            dataSourceBuilder.MapComposite<Location>();
            using var dataSource = dataSourceBuilder.Build();
            using var command = dataSource.CreateCommand("SELECT * FROM journals");
            using var reader = command.ExecuteReader();

            List<JournalViewModel> local_list = new List<JournalViewModel>();

            while (reader.Read())
            {
                local_list.Add(new JournalViewModel()
                {
                    journalid = reader.GetInt32(0),
                    title = reader.GetFieldValue<string>(2),
                    researchers = reader.GetFieldValue<string>(3),
                    subject = reader.GetFieldValue<string>(4),
                    length = reader.GetFieldValue<int>(5),
                    releasedate = reader.GetFieldValue<DateOnly>(6).ToString("yyyy-MM-dd"),
                    isavailable = reader.GetFieldValue<bool>(7),
                    description = reader.GetString(8),
                });
                if (reader.GetFieldValue<bool>(7)) numAvailable++;
            }
            if (local_list.Count == 0)
            {
                return null;
            }
            journalListViewModel.Journals = local_list;
            journalListViewModel.NumAvailable = numAvailable;
            return journalListViewModel;
        }

        [HttpGet]
        public PartialViewResult GetJournals()
        {
            return PartialView("~/Views/Explore/_JournalView.cshtml", JournalToList());
        }

        [HttpGet]
        public IActionResult GetJournalList()
        {
            return Json(JournalToList() is null ? null : JournalToList().Journals);
        }

        //Movies
        [HttpGet]
        public MovieListViewModel? MovieToList()
        {
            var movieListViewModel = new MovieListViewModel();
            var numAvailable = 0;
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
                });
                if(reader.GetFieldValue<bool>(8)) numAvailable++;
            }
            if (local_list.Count == 0)
            {
                return null;
            }
            movieListViewModel.Movies = local_list;
            movieListViewModel.NumAvailable = numAvailable;
            return movieListViewModel;
        }

        [HttpGet]
        public PartialViewResult GetMovies()
        {
            return PartialView("~/Views/Explore/_MovieView.cshtml", MovieToList());
        }

        [HttpGet]
        public IActionResult GetMovieList()
        {
            return Json(MovieToList() is null ? null : MovieToList().Movies);
        }

        //Audiobooks
        [HttpGet]
        public AudiobookListViewModel? AudiobookToList()
        {
            var audiobookListViewModel = new AudiobookListViewModel();
            var numAvailable = 0;
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
                if(audiobook.availability == true) numAvailable++;
            }

            reader.Close();
            if (local_list.Count == 0)
            {
                return null;
            }
            audiobookListViewModel.Audiobooks = local_list;
            audiobookListViewModel.NumAvailable = numAvailable;
            return audiobookListViewModel;
        }

        [HttpGet]
        public PartialViewResult GetAudiobooks()
        {
            return PartialView("~/Views/Explore/_AudiobookView.cshtml", AudiobookToList());
        }

        [HttpGet]
        public IActionResult GetAudiobookList()
        {
            return Json(AudiobookToList() is null ? null : AudiobookToList().Audiobooks);
        }
    }

}
