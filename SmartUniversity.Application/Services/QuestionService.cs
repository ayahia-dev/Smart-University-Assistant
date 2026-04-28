using SmartUniversity.Application.DTOs.Question;
using SmartUniversity.Application.Interfaces;
using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _repo;

        public QuestionService(IQuestionRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateAsync(CreateQuestionDto dto)
        {
            if (dto.Options == null || dto.Options.Count < 2)
                throw new Exception("At least 2 options required");

            if (!dto.Options.Any(x => x.IsCorrect))
                throw new Exception("At least one correct answer required");

            var question = new Question(
                dto.QuizId,
                dto.Text,
                dto.Grade
            );

            foreach (var opt in dto.Options)
            {
                var option = new QuestionOption(opt.Text, opt.IsCorrect);
                question.AddOption(option);
            }

            await _repo.AddAsync(question);
        }

        public async Task<IEnumerable<QuestionDto>> GetByQuizAsync(int quizId)
        {
            var questions = await _repo.GetByQuizWithOptionsAsync(quizId);

            return questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                Text = q.Text,
                Grade = q.Grade,
                Options = q.Options.Select(o => new OptionDto
                {
                    Id = o.Id,
                    Text = o.Text
                }).ToList()
            });
        }
    }
}
