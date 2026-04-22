using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Quiz
{
    public class AnswerDto
    {
        public int QuestionId { get; set; }

        public int SelectedOptionId { get; set; }
    }
}
