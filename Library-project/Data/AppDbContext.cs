using Library_project.Data.Objects;
using Library_project.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_project.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<media> medias { get; set; }
        public DbSet<audiobook> audioBooks { get; set; }
        public DbSet<book> books { get; set; }
        public DbSet<journal> journals { get; set; }
        public DbSet<movie> movie { get; set; }


        protected readonly IConfiguration configuration;
        public AppDbContext(IConfiguration _config)
        {
            configuration = _config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(configuration.GetConnectionString("my_library"));
        }


        
        
      
        



    }
}
