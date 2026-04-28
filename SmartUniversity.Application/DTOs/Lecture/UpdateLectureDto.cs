using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Lecture
{
    public class UpdateLectureDto
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Room { get; set; }
        public string Instructor { get; set; }

        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
