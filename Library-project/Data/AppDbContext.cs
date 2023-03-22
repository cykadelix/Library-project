using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_project.Models
{
    //startup.cs add DbContext ???
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {   
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasKey(e => new { e.employeeID});
            modelBuilder.Entity<Employee>().HasKey(e => new { e.supervisorID});   //foreign key setup??

            modelBuilder.Entity<Historian>().HasKey(h => new { h.historianID});


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> employees { get; set; }
        public DbSet<Historian> historians { get; set; }
    }
}
