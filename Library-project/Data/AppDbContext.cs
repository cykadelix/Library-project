using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Library_project.Models;

namespace Library_project.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {   
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //employee
            modelBuilder.Entity<Employee>().HasKey(e => new { e.employeeID });
            modelBuilder.Entity<Employee>().HasKey(e => new { e.supervisorID });   //foreign key setup??
            //historian
            modelBuilder.Entity<Historian>().HasKey(h => new { h.historianID});
            //review
            modelBuilder.Entity<Review>().HasKey(r => new { r.reviewID});
            //checkout
            modelBuilder.Entity<Checkout>().HasKey(c => new { c.checkoutID});


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> employees { get; set; }
        public DbSet<Historian> historians { get; set; }
    }
}
