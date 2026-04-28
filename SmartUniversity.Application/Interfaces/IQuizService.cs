using SmartUniversity.Application.DTOs.Quiz;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.Interfaces
{
    public interface IQuizService
    {
        Task CreateAsync(CreateQuizDto dto);
        Task<IEnumerable<QuizDto>> GetByCourseAsync(int courseId);
        Task UpdateAsync(int id, UpdateQuizDto dto);
        Task DeleteAsync(int id);
        Task StartQuizAsync(int quizId, int studentId);
    }
}
