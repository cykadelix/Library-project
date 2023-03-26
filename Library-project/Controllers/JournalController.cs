using Library_project.Data.Objects;
using Library_project.Models;
using Library_project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Library_project.Controllers
{
    public class JournalController : Controller
    {
        private readonly string connString = "Host=127.0.0.1;Server=localhost;Port=5432;Database=my_library;UserID=postgres;Password=killer89;Pooling=true";
        public async Task<IActionResult> Index()
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connString);
            
            dataSourceBuilder.MapComposite<Location>();
            await using var dataSource = dataSourceBuilder.Build();
            await using var command = dataSource.CreateCommand("SELECT * FROM journal ,media WHERE journalid=media.mediaId");
            await using var reader = await command.ExecuteReaderAsync();

            var journalList = new JournalListViewModel();
            List<journal> local_list = new List<journal>();

            while(await reader.ReadAsync())
            {
                local_list.Add(new journal()
                {
                    jouranalid = (int)reader["journalid"],
                    title = (string)reader["title"],
                    researchers = reader.GetFieldValue<string[]>(5),
                    subject = reader.GetFieldValue<string[]>(6),
                    length = (int)reader["length"],
                    releasedate = reader.GetFieldValue<DateOnly>(4)
                }) ; ;
            }
            journalList.allJournals = local_list;
            return View(journalList);
            
        }
        public IActionResult JournalForm()
        {
            return View();
        }

    }
}
