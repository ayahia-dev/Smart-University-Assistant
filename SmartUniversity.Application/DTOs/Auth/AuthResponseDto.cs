using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
