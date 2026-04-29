using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Grade
{
    public class AddFinalDto
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public double Final { get; set; }
    }
}
