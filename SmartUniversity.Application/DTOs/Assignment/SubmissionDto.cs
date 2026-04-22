using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Assignment
{
    public class SubmissionDto
    {
        public int Id { get; set; }

        public string StudentName { get; set; }

        public string FileUrl { get; set; }

        public DateTime SubmittedAt { get; set; }

        public double? Grade { get; set; }
        public string? Feedback { get; set; }
    }
}
