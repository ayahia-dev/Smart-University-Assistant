using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Assignment
{
    public class CreateAssignmentDto
    {
        public int CourseId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime DueDate { get; set; }   
        public double MaxGrade { get; set; }    
    }
}
