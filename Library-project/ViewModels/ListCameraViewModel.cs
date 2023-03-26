using Library_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_project.ViewModels
{
    public class ListCameraViewModel : Controller
    {
        public List<camera> Cameras = new List<camera>();
    }   
}
