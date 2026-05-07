using SmartUniversity.Application.DTOs.Course;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.Interfaces
{
    public interface ICourseService
    {
        Task<object> GetPagedAsync(string? name, int page, int pageSize);
        Task CreateAsync(CreateCourseDto dto);
        Task SetGrading(int courseId, double maxMid, double maxFinal);
        Task<List<CourseDto>> GetDoctorCourses(int doctorId);
        Task DeleteAsync(int id);
        Task<List<CourseDto>> GetByFaculty(int facultyId);
    }
}
