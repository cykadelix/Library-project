using Library_project.Data.Enums;
using Library_project.Data.Objects;
using Library_project.Models;
using Library_project.ViewModels.Movie;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Library_project.Controllers
{
    public class MovieController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public MovieController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public async Task<IActionResult> Index()
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder("Server=azurelibrarydatabase.postgres.database.azure.com;Database=Library;Port=5432;User Id=chavemm;Password=Postgres-2023!;Ssl Mode=Allow;");
            dataSourceBuilder.MapEnum<genres>();
            dataSourceBuilder.MapComposite<Location>();
            await using var dataSource = dataSourceBuilder.Build();
            await using var command = dataSource.CreateCommand("SELECT * FROM movie ,media WHERE movieid=media.mediaId");
            await using var reader = await command.ExecuteReaderAsync();

            var movieList = new MovieListViewModel();
            var local_list = new List<movie>();
            while(await reader.ReadAsync())
            {
                local_list.Add(new movie()
                {
                    movieId = (int)reader["mediaid"],
                    rating = (int)reader["rating"],
                    title = (string)reader["title"],
                    director = (string)reader["director"],
                    genres = (int)reader["genres"],
                    length = (int)reader["length"],
                    releasedate=reader.GetFieldValue<DateOnly>(6),
                    availability = (bool)reader["availability"]

                });

            }
            movieList.allMovies = local_list;
            return View(movieList);
        }
    }
}
