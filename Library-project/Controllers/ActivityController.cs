using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library_project.Data;
using Library_project.Interfaces;
using Library_project.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library_project.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityRepository activityRepository;

        public ActivityController(IActivityRepository _activityRepository)
        {
            activityRepository = _activityRepository;
        }
           
        public async Task<IActionResult> Index()
        {
            IEnumerable<activities> activities = await activityRepository.GetAll();
            return View(activities);
        }

        public async Task<IActionResult> Detail(int rno)
        {
            activities activity = await activityRepository.GetByRoomAsync(rno);
            return View(activity);
        }
    }
}

