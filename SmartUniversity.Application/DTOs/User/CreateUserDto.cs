using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.User
{
    public class CreateUserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string NationalId { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int FacultyId { get; set; }
    }
}
