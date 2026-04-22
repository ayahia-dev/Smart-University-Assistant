using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Question
{
    public class QuestionDto
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public double Grade { get; set; }

        public List<OptionDto> Options { get; set; }
    }
}
