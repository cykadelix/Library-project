using System;
namespace Library_project.Models
{
    public class Event
    {
        public DateTime Date { get; set; }
        public int EventID { get; set; }
        public string Description { get; set; }
        public int Length { get; set; }
        public List<int> RoomNumbers { get; set; }
        public EventType EventType { get; set; }

        public Event(DateTime date, int eventId, string description, int length, List<int> roomNumbers, EventType eventType)
        {
            Date = date;
            EventID = eventId;
            Description = description;
            Length = length;
            RoomNumbers = roomNumbers;
            EventType = eventType;
        }
    }

    public enum EventType
    {
        Movie_Night,
        Author_Signing,
        Book_Fair,
        Children_Reading_Hour,
        Charity_Fundraiser,
        Performance
    }

}

