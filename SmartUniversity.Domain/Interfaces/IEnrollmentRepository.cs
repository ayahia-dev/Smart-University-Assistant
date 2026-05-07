using SmartUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task AddAsync(Enrollment enrollment);

        Task<bool> ExistsAsync(int studentId, int courseId);

        Task<List<Enrollment>> GetByStudentIdAsync(int studentId);
        Task<List<User>> GetStudentsByCourseIdAsync(int courseId);
        Task<Enrollment?> GetAsync(int studentId, int courseId);

        Task DeleteAsync(Enrollment enrollment);
    }
}

