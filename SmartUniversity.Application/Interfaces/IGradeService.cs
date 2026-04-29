using SmartUniversity.Application.DTOs.Grade;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.Interfaces
{
    public interface IGradeService
    {
        Task<StudentGradeResponseDto> GetStudentGrade(int studentId, int courseId);
    }
}
