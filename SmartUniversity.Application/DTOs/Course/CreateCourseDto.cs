using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Course
{
    public class CreateCourseDto
    {
        public string Code { get; set; }   
        public string Name { get; set; }
        public string Description { get; set; }
        public int DoctorId { get; set; }
        public int FacultyId { get; set; }
    }
}
