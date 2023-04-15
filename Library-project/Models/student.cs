using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Library_project.Models
{
    public class student : IdentityUser
    {
        [Key]
        public int id { get; set; }
    }
}
