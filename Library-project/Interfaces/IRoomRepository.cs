using System;
using Library_project.Models;

namespace Library_project.Interfaces
{
	public interface IRoomRepository
	{
        Task<IEnumerable<rooms>> GetAll();
        Task<IEnumerable<rooms>> GetAllByRoomAsync(activities activity);

        //CRUD
        bool Add(rooms room);
        bool Update(rooms room);
        bool Delete(rooms room);
        bool Save();
    }
}

