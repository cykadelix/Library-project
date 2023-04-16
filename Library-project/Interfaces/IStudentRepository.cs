using System;
using Library_project.Models;

namespace Library_project.Interfaces
{
	public interface IStudentRepository
	{
        Task<IEnumerable<students>> GetAll();
        Task<IEnumerable<students>> GetAllByOverdueFeesAsync();

        //CRUD
        bool Add(students student);
        bool Update(students student);
        bool Delete(students student);
        bool Save();
    }
}

