using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Question
{
    public class CreateQuestionDto
    {
        public int QuizId { get; set; }

        public string Text { get; set; }

        public double Grade { get; set; }

        public List<CreateOptionDto> Options { get; set; }
    }
}
