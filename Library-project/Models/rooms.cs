﻿using System;
using System.Diagnostics;
using Library_project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_project.Models
{
	public class rooms
	{
        [Key]
        public int room_number { get; set; }

        //Room Info
        public int capacity { get; set; }
        public string? features { get; set; }
        public List<DateTime>? availabilty { get; set; }
        public bool reserved { get; set; }
        public ICollection<activities>? activites { get; } = new List<activities>();

        public rooms()
        {
        }
    }
}

