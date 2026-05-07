using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Student
{
    public class StudentDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string FacultyName { get; set; }

        public bool IsPublished { get; set; }
    }
}
