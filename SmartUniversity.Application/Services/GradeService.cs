using SmartUniversity.Application.DTOs.Grade;
using SmartUniversity.Application.Exceptions;
using SmartUniversity.Application.Interfaces;
using SmartUniversity.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.Services
{
    public class GradeService : IGradeService
    {
        private readonly IStudentGradeRepository _gradeRepo;
        private readonly ICourseRepository _courseRepo;

        public GradeService(
            IStudentGradeRepository gradeRepo,
            ICourseRepository courseRepo)
        {
            _gradeRepo = gradeRepo;
            _courseRepo = courseRepo;
        }

        public async Task<StudentGradeResponseDto> GetStudentGrade(int studentId, int courseId)
        {
            var grade = await _gradeRepo.GetByStudentAndCourse(studentId, courseId);
            var course = await _courseRepo.GetByIdAsync(courseId);

            if (grade == null)
                throw new BadRequestException("Grade not found");

            return new StudentGradeResponseDto
            {
                Midterm = grade.MidtermGrade,
                Final = grade.FinalGrade,
                Total = grade.MidtermGrade + grade.FinalGrade,

                MaxMidterm = course.MaxMidterm,
                MaxFinal = course.MaxFinal
            };
        }
    }
}
