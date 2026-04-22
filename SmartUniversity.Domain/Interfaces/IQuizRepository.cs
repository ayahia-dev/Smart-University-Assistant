using SmartUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Interfaces
{
    public interface IQuizRepository
    {
        Task AddAsync(Quiz quiz);

        Task<List<Quiz>> GetByCourseIdAsync(int courseId);
        Task<Quiz?> GetByIdAsync(int id);
    }
}
