using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Quiz
{
    public class QuestionResultDto
    {
        public string Question { get; set; }

        public string SelectedAnswer { get; set; }

        public string CorrectAnswer { get; set; }

        public bool IsCorrect { get; set; }

        public double Grade { get; set; }
    }
}
