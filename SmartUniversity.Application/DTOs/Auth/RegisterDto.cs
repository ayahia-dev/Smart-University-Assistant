using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartUniversity.Application.DTOs.Auth
{
    public class RegisterDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string NationalId { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public int FacultyId { get; set; }
    }
}
