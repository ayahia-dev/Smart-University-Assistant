using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Question
{
    public class OptionDto
    {
        public int Id { get; set; }

        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
