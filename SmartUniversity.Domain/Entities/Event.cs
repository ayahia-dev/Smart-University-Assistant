using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Entities
{
    public class Event
    {
        public int Id { get; private set; }

        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;

        public DateTime EventDate { get; private set; }

        private Event() { }

        public Event(string title, string description, DateTime eventDate)
        {
            Title = title;
            Description = description;
            EventDate = eventDate;
        }

        public void Update(string title, string description, DateTime eventDate)
        {
            Title = title;
            Description = description;
            EventDate = eventDate;
        }
    }
}
