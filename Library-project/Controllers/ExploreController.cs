﻿using Microsoft.AspNetCore.Mvc;
using Library_project.Models;
using Npgsql;
using Library_project.ViewModels;
using Library_project.Data.Enums;
using Library_project.Data.Objects;

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

        [HttpGet]
        public PartialViewResult GetCameras()
        {
            ListCameraViewModel cameraList = new ListCameraViewModel();
            using (var conn = new NpgsqlConnection(_config["ConnectionString"]))
            {

                Console.Out.WriteLine("Opening connection");
                conn.Open();


                using (var command = new NpgsqlCommand("SELECT * FROM camera", conn))
                {

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        camera cam = new camera();
                        cam.brand = reader.GetString(0);
                        cam.serialnumber = reader.GetInt32(1);
                        cam.description = reader.GetString(2);
                        cam.lumens = reader.GetInt32(3);
                        cam.availibility = reader.GetBoolean(4);
                        cameraList.Cameras.Add(cam);
                    }
                    reader.Close();
                }
            }
            return PartialView("~/Views/Explore/_CameraView.cshtml", cameraList);
        }

        [HttpPost]
        public IActionResult AddCamera(camera camera)
        {

            return RedirectToAction("AddMediaForm", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config["ConnectionString"]);
            dataSourceBuilder.MapEnum<genres>();
            dataSourceBuilder.MapComposite<Location>();
            await using var dataSource = dataSourceBuilder.Build();
            await using var command = dataSource.CreateCommand("SELECT * FROM book ,media WHERE bookId=media.mediaId");
            await using var reader = await command.ExecuteReaderAsync();

            var bookList = new ListBookViewModel();
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

        public IActionResult CreateBookView()
        {
            var newBook = new CreateBookViewModel();

            return View(newBook);
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
                            new("author", newBook.author.Split(',')),
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
    }
}
