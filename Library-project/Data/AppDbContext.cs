using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Library_project.Models;
<<<<<<< HEAD
﻿using Library_project.Data.Objects;
=======
using Library_project.Data.Objects;
using Microsoft.EntityFrameworkCore;
>>>>>>> origin/master

namespace Library_project.Data
{
    public class AppDbContext : DbContext
    {
<<<<<<< HEAD
        public DbSet<media> cameras { get; set; }
        public DbSet<employee> employees { get; set; }
        public DbSet<historian> historians { get; set; }
        public DbSet<student> students { get; set; }
        public DbSet<media> medias { get; set; }
        public DbSet<audiobook> audiobooks { get; set; }
        public DbSet<book> books { get; set; }
        public DbSet<journal> journals { get; set; }
        public DbSet<movie> movies { get; set; }
        public DbSet<projector> projectors { get; set;}
        public DbSet<computer> computers { get; set; }
        public DbSet<audiobook> checkouts { get; set; }
        public DbSet<review> reviews { get; set; }
=======
        public DbSet<cameras> cameras { get; set; }
        public DbSet<employees> employees { get; set; }
        public DbSet<historians> historians { get; set; }
        public DbSet<students> students { get; set; }


        public DbSet<medias> medias { get; set; }
        public DbSet<audiobooks> audioBooks { get; set; }
        public DbSet<books> books { get; set; }
        public DbSet<journals> journals { get; set; }
        public DbSet<movies> movies { get; set; }

        public DbSet<projectors> projectors { get; set;}
        
        public DbSet<computers> computers { get; set; }


        public DbSet<audiobooks> checkouts { get; set; }
        public DbSet<reviews> reviews { get; set; }
>>>>>>> origin/master
       



        public readonly IConfiguration configuration;
        public AppDbContext(IConfiguration _config)
        {
            configuration = _config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
<<<<<<< HEAD
            options.UseNpgsql(configuration.GetConnectionString("libraryConnectionString"));
=======
            options.UseNpgsql("Host=127.0.0.1;Server=localhost;Port=5432;Database=my_library;UserID=postgres;Password=killer89;Pooling=true");
>>>>>>> origin/master
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);
            mb.Entity<Location>().HasNoKey();
        }

        public AppDbContext() { }





    }
}