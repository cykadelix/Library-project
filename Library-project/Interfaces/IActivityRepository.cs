using System;
using Library_project.Models;

namespace Library_project.Interfaces
{
	public interface IActivityRepository
	{
        Task<IEnumerable<activities>> GetAll();
        Task<activities> GetByRoomAsync(int rno);
        Task<IEnumerable<activities>> GetActivityByLength(TimeOnly length);

        //CRUD
        bool Add(activities activity);
        bool Update(activities activity);
        bool Delete(activities activity);
        bool Save();
    }
}

