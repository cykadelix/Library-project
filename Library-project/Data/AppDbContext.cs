using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Library_project.Models;
﻿using Library_project.Data.Objects;
using Microsoft.EntityFrameworkCore;

namespace Library_project.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<camera> cameras { get; set; }
        public DbSet<employee> employees { get; set; }
        public DbSet<historian> historians { get; set; }
        public DbSet<student> students { get; set; }


        public DbSet<media> medias { get; set; }
        public DbSet<audiobook> audioBooks { get; set; }
        public DbSet<book> books { get; set; }
        public DbSet<journal> journals { get; set; }
        public DbSet<movie> movies { get; set; }

        public DbSet<projector> projectors { get; set;}
        
        public DbSet<computer> computers { get; set; }


        public DbSet<audiobook> checkouts { get; set; }
        public DbSet<review> reviews { get; set; }
       


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
        }

        public AppDbContext() { }
        
      
        

        
    }
}
