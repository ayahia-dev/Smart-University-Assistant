using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Quiz
{
    public class SubmitQuizDto
    {
        public int QuizId { get; set; }

        public List<AnswerDto> Answers { get; set; }
    }
}
