using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Question
{
    public class CreateOptionDto
    {
        public string Text { get; set; }

        public bool IsCorrect { get; set; }
    }
}
