using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Grade
{
    public class AddMidtermDto
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public double Midterm { get; set; }
    }
}
