using SmartUniversity.Application.DTOs.Course;
using SmartUniversity.Application.Interfaces;
using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repo;

        public CourseService(ICourseRepository repo)
        {
            _repo = repo;
        }

        public async Task<object> GetPagedAsync(string? name, int page, int pageSize)
        {
            var courses = await _repo.GetPagedAsync(name, page, pageSize);
            var total = await _repo.CountAsync(name);

            var data = courses.Select(x => new CourseDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                FacultyId = x.FacultyId,
                DoctorId = x.DoctorId
            });

            return new
            {
                total,
                page,
                pageSize,
                data
            };
        }

        public async Task CreateAsync(CreateCourseDto dto)
        {
            if (dto.DoctorId <= 0)
                throw new Exception("Invalid doctor");

            if (dto.FacultyId <= 0)
                throw new Exception("Invalid faculty");

            var course = new Course(
                dto.Code,
                dto.Name,
                dto.Description,
                dto.DoctorId,
                dto.FacultyId
            );

            await _repo.AddAsync(course);
        }
        public async Task SetGrading(int courseId, double maxMid, double maxFinal)
        {
            var course = await _repo.GetByIdAsync(courseId);

            if (course == null)
                throw new Exception("Course not found");

            course.SetGradingScheme(maxMid, maxFinal);

            await _repo.UpdateAsync(course);
        }
        public async Task<List<CourseDto>> GetDoctorCourses(int doctorId)
        {
            var courses = await _repo.GetByDoctorIdAsync(doctorId);

            return courses.Select(x => new CourseDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                FacultyId = x.FacultyId,
                DoctorId = x.DoctorId
            }).ToList();
        }
    }
}
