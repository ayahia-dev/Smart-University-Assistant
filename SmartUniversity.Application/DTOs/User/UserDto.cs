using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int FacultyId { get; set; }
        public string NationalId { get; set; }
        public string FacultyName { get; set; }
    }
}
