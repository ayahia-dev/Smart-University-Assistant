using System;

namespace SmartUniversity.Application.DTOs.Lecture
{
    public class LectureDto
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Subtitle { get; set; }

        public string Room { get; set; }

        public DayOfWeek Day { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}