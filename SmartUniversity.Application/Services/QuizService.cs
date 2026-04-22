using SmartUniversity.Application.DTOs.Quiz;
using SmartUniversity.Application.Interfaces;
using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _repo;

        public QuizService(IQuizRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateAsync(CreateQuizDto dto)
        {
            var quiz = new Quiz(
                dto.CourseId,
                dto.Title,
                dto.TotalGrade,
                dto.QuizDate,
                dto.DurationMinutes
            );

            await _repo.AddAsync(quiz);
        }

        public async Task<IEnumerable<QuizDto>> GetByCourseAsync(int courseId)
        {
            var quizzes = await _repo.GetByCourseIdAsync(courseId);

            return quizzes.Select(x => new QuizDto
            {
                Id = x.Id,
                Title = x.Title,
                TotalGrade = x.TotalGrade,
                QuizDate = x.QuizDate,
                DurationMinutes = x.DurationMinutes
            });
        }
    }
}
