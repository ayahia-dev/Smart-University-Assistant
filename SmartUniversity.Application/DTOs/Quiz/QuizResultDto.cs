using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Quiz
{
    public class QuizResultDto
    {
        public double TotalScore { get; set; }

        public List<QuestionResultDto> Questions { get; set; }
    }
}
