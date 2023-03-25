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
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Media>().HasKey(m => m.mediaId);



            modelBuilder.Entity<Movie>().HasOne(m => m.Media);
            modelBuilder.Entity<Movie>().HasKey(m => m.MovieId);

            modelBuilder.Entity<Book>().HasOne(m => m.Media);
            modelBuilder.Entity<Book>().HasKey(m => m.BookId);

            modelBuilder.Entity<AudioBook>().HasOne(m => m.Media);
            modelBuilder.Entity<AudioBook>().HasKey(m => m.AudioBookId);

            modelBuilder.Entity<Journal>().HasOne(m => m.Media);
            modelBuilder.Entity<Journal>().HasKey(m => m.JournalId);

            modelBuilder.Entity<Location>().HasKey(f => f.Floor);
            
            //employee
            modelBuilder.Entity<Employee>().HasKey(e => new { e.employeeID });
            modelBuilder.Entity<Employee>().HasKey(e => new { e.supervisorID });   //foreign key setup??
            //historian
            modelBuilder.Entity<Historian>().HasKey(h => new { h.historianID});
            //review
            modelBuilder.Entity<Review>().HasKey(r => new { r.reviewID});
            //checkout
            modelBuilder.Entity<Checkout>().HasKey(c => new { c.checkoutID});

            
        }

        public DbSet<Employee> employees { get; set; }
        public DbSet<Historian> historians { get; set; }
    }
}
