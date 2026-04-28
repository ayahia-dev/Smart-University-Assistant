using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Quiz
{
    public class UpdateQuizDto
    {
        public string Title { get; set; }
        public DateTime QuizDate { get; set; }
        public int DurationMinutes { get; set; }
    }
}
