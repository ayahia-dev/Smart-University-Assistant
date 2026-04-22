using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Quiz
{
    public class CreateQuizDto
    {
        public int CourseId { get; set; }

        public string Title { get; set; }

        public double TotalGrade { get; set; }

        public DateTime QuizDate { get; set; }

        public int DurationMinutes { get; set; }
    }
}
