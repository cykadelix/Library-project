using Library_project.Data.Objects;
using Library_project.Models;
using Library_project.ViewModels;
using Library_project.ViewModels.Journal;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Library_project.Controllers
{
    
    public class JournalController : Controller
    {
        private readonly IConfiguration _config;
        public JournalController(IConfiguration config)
        {
            _config = config;
        }
        
        public async Task<IActionResult> Index()
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder("Host=127.0.0.1;Server=localhost;Port=5432;Database=my_library;UserID=postgres;Password=killer89;Pooling=true");
            
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
                    researchers = reader.GetFieldValue<string>(5),
                    subject = reader.GetFieldValue<string>(6),
                    length = (int)reader["length"],
                    releasedate = reader.GetFieldValue<DateOnly>(4)
                }) ; ;
            }
            journalList.allJournals = local_list;
            return View(journalList);
            
        }
        [HttpGet]
        public IActionResult CreateJournalView()
        {
            var newJournal = new CreateJournalViewModel();
            return View(newJournal);
        }
        [HttpPost]
        public async Task<IActionResult> CreateJournalLandingPage(CreateJournalViewModel newJournal)
        {
            CreateJournalViewModel localJournal = new CreateJournalViewModel()
            {
                title = (string)newJournal.title,
                researchers=newJournal.researchers,
                subject = newJournal.subject,
                length = newJournal.length,
                releasedate = newJournal.releasedate,
                isavailable = (bool)newJournal.isavailable,
            };
            if (ModelState.IsValid)
            {
                
                await using NpgsqlConnection conn = new NpgsqlConnection(_config["ConnectionString"]);
                await conn.OpenAsync();

                await using var command = new NpgsqlCommand("WITH local_id AS (INSERT INTO media VALUES (DEFAULT,(2,1)) RETURNING mediaid) " +
                            "INSERT INTO journal (mediaid,title,length,datereleased,researchers,subject,journalid,isavailable) VALUES(" +
                            "(SELECT mediaid from local_id), " +
                            "@Title, " +
                            "@Length, " +
                            "@DateReleased, " +
                            "@Researchers, " +
                            "@Subject, " +
                            "(SELECT mediaid from local_id), " +
                            "@IsAvailable)", conn)
                {
                    Parameters =
                        {
                           new("Title",localJournal.title),
                           new("Researchers",localJournal.researchers),
                           new("Subject",localJournal.researchers),
                           new("IsAvailable",true),
                           new("DateReleased",localJournal.releasedate),
                           new("Length",localJournal.length)
                        }

                };
                await using var reader = await command.ExecuteReaderAsync();

            }
            else
            {

                localJournal.title = "fake";

            }
            return View(localJournal);
            
        }


    }
}
