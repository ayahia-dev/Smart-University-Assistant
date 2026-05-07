using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Grade
{
    public class StudentResultDto
    {
        public string CourseName { get; set; }

        public string DoctorName { get; set; }

        public double Midterm { get; set; }

        public double Final { get; set; }

        public double Total { get; set; }

        public bool IsPublished { get; set; }
    }
}
