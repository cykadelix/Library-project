using System;
namespace Library_project.Models
{
    public class Room
    {
        public int Capacity { get; set; }
        public Dictionary<DayOfWeek, Dictionary<TimeSpan, bool>> Availability { get; set; }
        public List<string> Features { get; set; }

        public Room(int capacity, Dictionary<DayOfWeek, Dictionary<TimeSpan, bool>> availability, List<string> features)
        {
            Capacity = capacity;
            Availability = availability;
            Features = features;
        }
    }


}

