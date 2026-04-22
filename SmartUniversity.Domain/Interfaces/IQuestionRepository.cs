using SmartUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Interfaces
{
    public interface IQuestionRepository
    {
        Task AddAsync(Question question);

        Task<List<Question>> GetByQuizIdAsync(int quizId);
        Task<List<Question>> GetByQuizWithOptionsAsync(int quizId);
    }
}
