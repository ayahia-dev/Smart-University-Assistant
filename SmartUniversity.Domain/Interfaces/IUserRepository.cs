using SmartUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetPagedAsync(string? name, string? role, int page, int pageSize);
        Task<int> CountAsync(string? name, string? role);

        Task<User?> GetByIdAsync(int id);
        Task AddAsync(User user);

        Task<bool> ExistsByEmailAsync(string email);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<bool> HasCoursesAsync(int doctorId);
        Task<List<User>> GetStudentsByFacultyAsync(int facultyId);
    }
}
