using SmartUniversity.Application.DTOs.Question;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.Interfaces
{
    public interface IQuestionService
    {
        Task CreateAsync(CreateQuestionDto dto);

        Task<IEnumerable<QuestionDto>> GetByQuizAsync(int quizId);
    }
}
