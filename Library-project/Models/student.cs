using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Library_project.Models
{
    public class students
    {
        [Key]
        public string id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
    }
}
