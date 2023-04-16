﻿using System;
using Library_project.Interfaces;
using Library_project.Models;
using Library_project.Data;
using Microsoft.EntityFrameworkCore;

namespace Library_project.Repository
{
	public class ActivityRepository : IActivityRepository
    {
        private readonly AppDbContext context;

        public ActivityRepository(AppDbContext _context)
        {
            context = _context;
        }

        //Searches all activities and returns a list of activities with the given length
        public async Task<IEnumerable<activity>> GetActivityByLength(TimeOnly length)
        {
            return await context.activities.Where(a => a.length == length).ToListAsync();
        }

        //Asyns must always return a Task object
        public async Task<IEnumerable<activity>> GetAll()
        {   
           return await context.activities.ToListAsync();
        }

        //Returns a activity with that is going to happen in the given room number, rno
        public async Task<activity> GetByRoomAsync(int rno)
        {
            return await context.activities.FirstOrDefaultAsync(i => i.room_number == rno);
        }

        public bool Add(activity activity)
        {
            //Generates SQL to create a new activity model
            context.Add(activity);
            return Save();  //Executes SQL query generated by add
        }

        public bool Delete(activity activity)
        {
            context.Remove(activity);
            return Save();
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(activity activity)
        {
            throw new NotImplementedException();
        }
    }
}

