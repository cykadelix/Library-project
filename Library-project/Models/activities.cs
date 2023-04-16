using System;
using Library_project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_project.Models
{
	public class activities
	{
        [Key]
        public int activity_id { get; set; }

        public DateOnly date { get; set; }
        public string? description { get; set; }
        public TimeOnly length { get; set; }
        public string? activity_type { get; set; } = null!;

        [ForeignKey("room")]
        public int room_number { get; set; }
        public rooms? room { get; set; }

        public activities()
		{
		}
	}
}

