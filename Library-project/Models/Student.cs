using System;
namespace Library_project.Models
{
	public class Student
	{
		public string Name { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string PhoneNumber { get; set; }
		public string LibraryCardNumber { get; set; }
		public int Age { get; set; }
		public decimal OverdueFees { get; set; }
		public List<int> CheckoutIDs { get; set; }
		public DateTime Birthday { get; set; }
		
		public Student()
		{
			Name = "";
			Address = "";
			Email = "";
			Password = "";
			PhoneNumber = "";
			LibraryCardNumber = "";
			Age = 0;
			OverdueFees = 0.0m;
			CheckoutIDs = Enumerable.Repeat(0, 10).ToList();

		}

	}

}

