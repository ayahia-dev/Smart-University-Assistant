using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.AI
{
    public class StudentAiDto
    {
        public string Name { get; set; }
        public double Attendance { get; set; }
        public double Assignments { get; set; }
        public double Quiz { get; set; }
        public double Trend { get; set; }
    }
}
