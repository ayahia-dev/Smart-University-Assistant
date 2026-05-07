using SmartUniversity.Application.DTOs.Course;
using SmartUniversity.Application.Interfaces;
using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using SmartUniversity.Application.Exceptions;

namespace SmartUniversity.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repo;
        private readonly IGenericRepository<Course> _genericRepo;
        private readonly ICourseChecker _courseChecker;
        public CourseService(ICourseRepository repo,IGenericRepository<Course> genericRepo,ICourseChecker courseChecker)
        {
            _repo = repo;
            _genericRepo = genericRepo;
            _courseChecker = courseChecker;
        }

        public async Task<object> GetPagedAsync(string? name, int page, int pageSize)
        {
            var courses = await _repo.GetPagedAsync(name, page, pageSize);
            var total = await _repo.CountAsync(name);

            var data = courses.Select(x => new CourseDto
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Description = x.Description,
                DoctorId = x.DoctorId,
                FacultyId = x.FacultyId,

                DoctorName = x.Doctor.FullName,
                FacultyName = x.Faculty.Name
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
        public async Task DeleteAsync(int id)
        {
            var course = await _repo.GetByIdAsync(id);

            if (course == null)
                throw new BadRequestException("Course not found");

            if (await _courseChecker.HasEnrollmentsAsync(id))
                throw new BadRequestException(
                    "Cannot delete course because students are enrolled in it");

            _genericRepo.Delete(course);

            await _genericRepo.SaveChangesAsync();
        }
        public async Task<List<CourseDto>> GetByFaculty(int facultyId)
        {
            var courses = await _repo.GetByFacultyIdAsync(facultyId);

            return courses.Select(x => new CourseDto
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code
            }).ToList();
        }
    }
}
