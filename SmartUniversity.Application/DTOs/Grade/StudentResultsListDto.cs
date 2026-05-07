using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Grade
{
    public class StudentResultsListDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string FacultyName { get; set; }

        public bool IsPublished { get; set; }
    }
}
