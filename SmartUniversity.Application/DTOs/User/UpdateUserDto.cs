using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.User
{
    public class UpdateUserDto
    {
        public string FullName { get; set; }
        public int FacultyId { get; set; }
        public string Role { get; set; }
        public string NationalId { get; set; }
    }
}
