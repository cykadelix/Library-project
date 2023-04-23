using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Library_project.Models;
using Library_project.Data.Objects;

namespace Library_project.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<cameras> cameras { get; set; }
        public DbSet<employees> employees { get; set; }
        public DbSet<historians> historians { get; set; }
        public DbSet<students> students { get; set; }
        public DbSet<medias> medias { get; set; }
        public DbSet<audiobooks> audiobooks { get; set; }
        public DbSet<books> books { get; set; }
        public DbSet<journals> journals { get; set; }
        public DbSet<movies> movies { get; set; }
        public DbSet<projectors> projectors { get; set; }
        public DbSet<computers> computers { get; set; }
        public DbSet<checkouts> checkouts { get; set; }
        public DbSet<reviews> reviews { get; set; }
        public DbSet<user> user { get; set; }
        public DbSet<userdto> userdtos { get; set; }

        public readonly IConfiguration configuration;
        public AppDbContext(IConfiguration _config)
        {
            configuration = _config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(configuration.GetConnectionString("local_lib"));
        }


        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);
            mb.Entity<Location>().HasNoKey();
            mb.Entity<userdto>().HasNoKey();
        }

        public AppDbContext() { }





    }
}