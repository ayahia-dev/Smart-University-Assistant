using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Attendance
{
    public class StudentAttendanceDto
    {
        public int StudentId { get; set; }
        public bool IsPresent { get; set; }
    }
}
