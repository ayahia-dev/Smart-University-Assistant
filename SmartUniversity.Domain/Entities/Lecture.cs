using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Entities
{
    public class Lecture
    {
        public int Id { get; private set; }

        public int CourseId { get; private set; }
        public Course Course { get; private set; }

        public string Title { get; private set; } = string.Empty;
        public string Subtitle { get; private set; } = string.Empty;

        public string Room { get; private set; } = string.Empty;
        public string Instructor { get; private set; } = string.Empty;

        public DayOfWeek Day { get; private set; } 

        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime { get; private set; }

        private Lecture() { }

        public Lecture(int courseId, string title, string subtitle,
            string room, string instructor,
            DayOfWeek day,
            TimeSpan start,
            TimeSpan end)
        {
            CourseId = courseId;
            Title = title;
            Subtitle = subtitle;
            Room = room;
            Instructor = instructor;
            Day = day;
            StartTime = start;
            EndTime = end;
        }

        public void Update(string title, string subtitle,
            string room, string instructor,
            DayOfWeek day,
            TimeSpan start,
            TimeSpan end)
        {
            Title = title;
            Subtitle = subtitle;
            Room = room;
            Instructor = instructor;
            Day = day;
            StartTime = start;
            EndTime = end;
        }
    }
}

