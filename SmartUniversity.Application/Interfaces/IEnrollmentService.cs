using SmartUniversity.Application.DTOs.Course;
using SmartUniversity.Application.DTOs.Enrollment;
using SmartUniversity.Application.DTOs.Student;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.Interfaces
{
    public interface IEnrollmentService
    {
        Task EnrollAsync(int studentId, int courseId);

        Task<IEnumerable<CourseDto>> GetMyCoursesAsync(int studentId);
        Task RemoveEnrollmentAsync(int studentId, int courseId);
    }
}
