using SmartUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Interfaces
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetPagedAsync(string? name, int page, int pageSize);
        Task<int> CountAsync(string? name);

        Task<Course?> GetByIdAsync(int id);
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task<List<Course>> GetByDoctorIdAsync(int doctorId);
        Task<List<Course>> GetByFacultyIdAsync(int facultyId);
    }
}
