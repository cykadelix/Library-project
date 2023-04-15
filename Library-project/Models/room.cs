using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Library_project.Models
{
    public class room
    {
        [Key]
        public int room_number { get; set; }
        public int capacity { get; set; }

        public string? features { get; set; }

        //Dates and times the room is available
        public DateTime availabilty { get; set; }
        public bool reserved { get; set; }

        public ICollection<activity>? activites { get; } = new List<activity>();
    }
}

