using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Grade
{
    public class StudentGradeResponseDto
    {
        public double Midterm { get; set; }
        public double Final { get; set; }

        public double MaxMidterm { get; set; }
        public double MaxFinal { get; set; }
        public double Total { get; set; }
    }
}
