using SmartUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Interfaces
{
    public interface IStudentGradeRepository
    {
        Task<StudentGrade?> GetAsync(int studentId, int courseId);
        Task<List<StudentGrade>> GetByCourseAsync(int courseId);

        Task AddAsync(StudentGrade grade);
        Task UpdateAsync(StudentGrade grade);
        Task<StudentGrade?> GetByStudentAndCourse(int studentId, int courseId);
        Task<List<StudentGrade>> GetByStudentIdAsync(int studentId);

    }
}
