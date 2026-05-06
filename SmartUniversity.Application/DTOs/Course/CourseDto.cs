using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Course
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DoctorId { get; set; }
        public int FacultyId { get; set; }
        public string DoctorName { get; set; }

        public string FacultyName { get; set; }
        public DateTime EnrolledAt { get; set; }
    }
}
