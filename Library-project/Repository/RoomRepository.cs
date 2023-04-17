using System;
using System.Diagnostics;
using Library_project.Data;
using Library_project.Interfaces;
using Library_project.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_project.Repository
{
	public class RoomRepository : IRoomRepository
	{
        private readonly AppDbContext context;

        public RoomRepository(AppDbContext _context)
		{
			context = _context;
		}

        public Task<IEnumerable<rooms>> GetAll()
        {
            throw new NotImplementedException();
        }

        //Returns a list of activities that will occur in a given room
        public async Task<IEnumerable<rooms>> GetAllByRoomAsync(activities activity)
        {
            return await context.rooms.Where(r => r.room_number == activity.room_number).ToListAsync();
        }

        public bool Add(rooms room)
        {
            context.Add(room);
            return Save();
        }

        public bool Delete(rooms room)
        {
            context.Remove(room);
            return Save();
        }

        public bool Update(rooms room)
        {
            context.Update(room);
            return Save();
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        
    }
}

