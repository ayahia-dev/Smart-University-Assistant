using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string NationalId { get; private set; } = string.Empty;
        public string FullName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public int FacultyId { get; private set; }
        public Faculty Faculty { get; private set; }
        public string PasswordHash { get; private set; } = string.Empty;
        public string Role { get; private set; } = string.Empty;

        public ICollection<Enrollment> Enrollments { get; private set; }
            = new List<Enrollment>();
        public ICollection<StudentGrade> StudentGrades { get; private set; }
        = new List<StudentGrade>();

        private User() { }

        public User(
                string fullName,
                string email,
                string nationalId,
                string passwordHash,
                string role,
                int facultyId)
        {
            FullName = fullName;
            Email = email;
            NationalId = nationalId;
            PasswordHash = passwordHash;
            Role = role;
            FacultyId = facultyId;
        }

        public void ChangePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
        }

        public void Deactivate()
        {
            Role = "Inactive";
        }
        public void UpdateInfo(string fullName,string nationalId,int facultyId)
        {
            FullName = fullName;

            NationalId = nationalId;

            FacultyId = facultyId;
        }

        public void ChangeRole(string role)
        {
            Role = role;
        }
    }
}
