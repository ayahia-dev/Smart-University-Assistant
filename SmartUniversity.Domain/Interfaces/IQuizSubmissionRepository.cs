using SmartUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Interfaces
{
    public interface IQuizSubmissionRepository
    {
        Task AddAsync(QuizSubmission submission);

        Task<bool> ExistsAsync(int quizId, int studentId);
        Task<QuizSubmission?> GetAsync(int quizId, int studentId);
        Task<List<double>> GetStudentQuizScores(int studentId);
        Task<bool> HasSubmissionsAsync(int quizId);
        Task SaveChangesAsync();
    }
}
