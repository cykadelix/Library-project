using System;
using System.Threading.Tasks;
using Library_project.Models;
namespace Library_project.Interfaces
{
	public interface IStudentRepository
	{
        Task<IEnumerable<student>> GetAll();
        Task<student> GetAllByOverdueFees();

        //CRUD
        bool Add(student student);
        bool Update(student student);
        bool Delete(student student);
        bool Save();
    }
  
}

