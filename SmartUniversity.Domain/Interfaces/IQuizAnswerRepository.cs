using SmartUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Interfaces
{
    public interface IQuizAnswerRepository
    {
        Task AddRangeAsync(List<QuizAnswer> answers);

        Task<List<QuizAnswer>> GetBySubmissionIdAsync(int submissionId);
    }
}
