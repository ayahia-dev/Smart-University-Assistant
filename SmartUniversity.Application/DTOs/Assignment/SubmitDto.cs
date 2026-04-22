using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Assignment
{
    public class SubmitDto
    {
        public int AssignmentId { get; set; }
        public IFormFile File { get; set; }
    }
}
