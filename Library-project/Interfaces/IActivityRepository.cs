using System;
using Library_project.Models;
namespace Library_project.Interfaces
{
	public interface IActivityRepository
	{
		Task<IEnumerable<activity>> GetAll();
		Task<activity> GetByRoomAsync();
		Task<IEnumerable<activity>> GetActivityByLength();

		//CRUD
		bool Add(activity activity);
        bool Update(activity activity);
        bool Delete(activity activity);
        bool Save();

    }
}

