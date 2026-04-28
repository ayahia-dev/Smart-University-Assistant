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

        public StudentGradeService(IStudentGradeRepository repo)
        {
            _repo = repo;
        }

        public async Task AddOrUpdate(int studentId, int courseId, double mid, double final)
        {
            var grade = await _repo.GetAsync(studentId, courseId);

            if (grade == null)
            {
                var newGrade = new StudentGrade(studentId, courseId, mid, final);
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

        public async Task<StudentGrade> GetStudentGrade(int studentId, int courseId)
        {
            var grade = await _repo.GetAsync(studentId, courseId);

            if (grade == null)
                throw new Exception("No grade found");

            if (!grade.IsPublished)
                throw new BadRequestException("Result not published yet");

            return grade;
        }
    }
}
