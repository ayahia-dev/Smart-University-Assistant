using SmartUniversity.Application.DTOs.Student;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.Interfaces
{
    public interface ICourseStudentQuery
    {
        Task<List<StudentDto>> GetStudentsByCourseIdAsync(int courseId);
    }
}
