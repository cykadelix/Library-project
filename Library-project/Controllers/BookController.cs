using Library_project.Data.Enums;

using Library_project.Models;
using Library_project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Library_project.Controllers
{
    public class BookController : Controller
    {
        private readonly string connString = "Host=127.0.0.1;Server=localhost;Port=5432;Database=my_library;UserID=postgres;Password=killer89;Pooling=true";
       

        
        public async Task<IActionResult> Index()
        {

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connString);
            dataSourceBuilder.MapEnum<genres>();

            await using var dataSource = dataSourceBuilder.Build();
            await using var command = dataSource.CreateCommand("SELECT * FROM book");
            await using var reader = await command.ExecuteReaderAsync();

            var bookList = new ListBookViewModel();
            var LocalList = new List<book>();
            while (await reader.ReadAsync())
            {
                LocalList.Add(  new book()
                {
                    bookId = (int)reader["bookId"],
                    title = reader["title"] as string,
                    mediaId = (int)reader["bookId"],
                    isAvailable = (bool)reader["isAvailable"],
                    isbn = (long)reader["isbn"],
                    pageCount = (int)reader["pageCount"],
                    publicDate = (DateTime)reader["publicDate"],
                    author = reader.GetFieldValue <string[]>(2),
                    genres = reader.GetFieldValue<List<genres>>(8),
                });

                bookList.allBooks = LocalList;
            }


            return View(bookList);

        }
        public IActionResult BookForm()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            var newBook = new ListBookViewModel();

            return View(newBook);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookViewModel newBook)
        {
            var book = new CreateBookViewModel();
            if (!ModelState.IsValid)
            {
                book.title = "fake";
            }
            else
            {
                book.title = newBook.title;

                await using var conn = new NpgsqlConnection("Host=127.0.0.1;Server=localhost;Port=5432;Database=myWorker;UserID=postgres;Password=killer89;Pooling=true");

                await conn.OpenAsync();
                await using var cmd = new NpgsqlCommand("WITH local_id AS (INSERT INTO media VALUES (DEFAULT, 1, 1) RETURNING media_id) INSERT INTO books VALUES((SELECT mediaId from local_id)," +
                    " (@title),(@author),(@genres),(@publicDate),(@pageCount),(@isbn),(@isAvailable), (SELECT media_id from local_id))", conn)
                {
                    Parameters =
                    {
                        
                        new("title", book.title),
                        new("author", book.author),

                        new("genres", book.title),
                        new("publicDate", book.title),
                        new("pageCount", book.title),
                        new("isbn", book.title),
                        new("isAvailable", book.title),
                        


                    }
                };
                await using var reader = await cmd.ExecuteReaderAsync();



            }

            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Edit(int book_id)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connString);

            await using var dataSource = dataSourceBuilder.Build();
            await using var command = dataSource.CreateCommand("SELECT * FROM books");
            await using var reader = await command.ExecuteReaderAsync();




            var localBook = new EditBookViewModel();
            reader.Read();
            using var innerRead = reader.GetData(0);

            while(innerRead.Read())
            {
                localBook.title = innerRead.GetFieldValue<string>(1);
                localBook.author = innerRead.GetFieldValue<string[]>(2);
                localBook.genres = innerRead.GetFieldValue<genres[]>(3);
                localBook.publicDate = innerRead.GetFieldValue<DateOnly>(4);
                localBook.pageCount = innerRead.GetFieldValue<int>(4);
                localBook.isbn = innerRead.GetFieldValue<int>(6);
                localBook.isAvailable = innerRead.GetFieldValue<bool>(7);
            }

            return View(localBook);
            
            
        }
        public async Task<IActionResult> Edit(EditBookViewModel editBookvm)
        {

            return RedirectToAction("Index");
        }
    }
}
