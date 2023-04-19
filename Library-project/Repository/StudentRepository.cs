using System;
using Library_project.Interfaces;
using Library_project.Models;
using Library_project.Data;
using Microsoft.EntityFrameworkCore;

namespace Library_project.Repository
{
	public class StudentRepository : IStudentRepository
	{
        private readonly AppDbContext context;

        public StudentRepository(AppDbContext _context)
		{
            context = _context;
		}

        //Returns all the students in the database
        public async Task<IEnumerable<students>> GetAll()
        {
            return await context.students.ToListAsync();
        }

        //Returns a list of students who have overdue fees
        public async Task<IEnumerable<students>> GetAllByOverdueFeesAsync()
        {
            //return await context.students.Where(s => s.overdue_fees > 0).ToListAsync();
            return null;
        }

        //CRUD Operations

        public bool Add(students student)
        {
            context.Add(student);
            return Save();
        }

        public bool Delete(students student)
        {
            context.Remove(student);
            return Save();
        }

        public bool Update(students student)
        {
            context.Update(student);
            return Save();
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        
    }
}

