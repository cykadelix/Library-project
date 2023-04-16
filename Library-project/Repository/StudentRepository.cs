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
        public async Task<IEnumerable<student>> GetAll()
        {
            return await context.students.ToListAsync();
        }

        //Returns a list of students based on their library card number who have overdue fees
        public async Task<IEnumerable<student>> GetAllByOverdueFees(int lcn)
        {
           return await context.students.Where(s => s.libr)
        }

        public bool Add(student student)
        {
            context.Add(student);
            return Save();
        }

        public bool Delete(student student)
        {
            context.Remove(student);
            return Save();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(student student)
        {
            throw new NotImplementedException();
        }
    }
}

