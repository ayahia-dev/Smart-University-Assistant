using SmartUniversity.Application.DTOs.Grade;
using SmartUniversity.Application.Exceptions;
using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.Services
{
    public class StudentGradeService
    {
        private readonly IStudentGradeRepository _repo;
        private readonly ICourseRepository _courseRepo;

        public StudentGradeService(
            IStudentGradeRepository repo,
            ICourseRepository courseRepo)
        {
            _repo = repo;
            _courseRepo = courseRepo;
        }

        public StudentGradeService(IStudentGradeRepository repo)
        {
            _repo = repo;
        }

        public async Task AddOrUpdate(int studentId, int courseId, double mid, double final)
        {
            var course = await _courseRepo.GetByIdAsync(courseId);

            if (course == null)
                throw new Exception("Course not found");

            if (mid > course.MaxMidterm)
                throw new BadRequestException("Midterm exceeds max");

            if (final > course.MaxFinal)
                throw new BadRequestException("Final exceeds max");

            var grade = await _repo.GetAsync(studentId, courseId);

            if (grade == null)
            {
                var newGrade = new StudentGrade(studentId, courseId);
                newGrade.Update(mid, final);

                await _repo.AddAsync(newGrade);
            }
            else
            {
                grade.Update(mid, final);
                await _repo.UpdateAsync(grade);
            }
        }

        public async Task PublishGrades(int courseId)
        {
            var grades = await _repo.GetByCourseAsync(courseId);

            foreach (var g in grades)
            {
                g.Publish();
            }

            foreach (var g in grades)
            {
                await _repo.UpdateAsync(g);
            }
        }

        public async Task<StudentGradeResponseDto> GetStudentGrade(int studentId, int courseId)
        {
            var grade = await _repo.GetAsync(studentId, courseId);
            var course = await _courseRepo.GetByIdAsync(courseId);

            if (grade == null)
                throw new BadRequestException("No grade found");

            if (!grade.IsPublished)
                throw new BadRequestException("Result not published yet");

            return new StudentGradeResponseDto
            {
                Midterm = grade.MidtermGrade,
                Final = grade.FinalGrade,

                MaxMidterm = course.MaxMidterm,
                MaxFinal = course.MaxFinal,

                Total = grade.MidtermGrade + grade.FinalGrade
            };
        }
        public async Task SetMidterm(int studentId, int courseId, double midterm)
        {
            var grade = await _repo.GetByStudentAndCourse(studentId, courseId);

            if (grade == null)
            {
                grade = new StudentGrade(studentId, courseId);
                grade.SetMidterm(midterm);
                await _repo.AddAsync(grade);
            }
            else
            {
                grade.SetMidterm(midterm);
                await _repo.UpdateAsync(grade);
            }
        }
        public async Task SetFinal(int studentId, int courseId, double final)
        {
            var grade = await _repo.GetByStudentAndCourse(studentId, courseId);

            if (grade == null)
            {
                grade = new StudentGrade(studentId, courseId);
                grade.SetFinal(final);
                await _repo.AddAsync(grade);
            }
            else
            {
                grade.SetFinal(final);
                await _repo.UpdateAsync(grade);
            }
        }
    }
}
