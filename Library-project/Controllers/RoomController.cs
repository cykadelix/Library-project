using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library_project.Data;
using Library_project.Interfaces;
using Library_project.Models;
using Library_project.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library_project.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomRepository roomRepository;

        RoomController(IRoomRepository _roomRepository)
        {
            roomRepository = _roomRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<rooms> rooms = await roomRepository.GetAll();
            return View(rooms);
        }
        public IActionResult RoomIndex()
        {
            //IEnumerable<students> students = await studentRepository.GetAll();
            return View();
        }

        public IActionResult CreateRoom()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(rooms roomInput)
        {
            //Creates a new student method
            if (!ModelState.IsValid)
            {
                return View(roomInput);
            }
            roomRepository.Add(roomInput);
            return RedirectToAction("RoomIndex");
        }
    }
}

