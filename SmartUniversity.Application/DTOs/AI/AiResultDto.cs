using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.AI
{
    public class AiResultDto
    {
        public string Name { get; set; }
        public double Academic_Score { get; set; }
        public string Risk_Level { get; set; }
        public string Fail_Probability { get; set; }
    }
}
