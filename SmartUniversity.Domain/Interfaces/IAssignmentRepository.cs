using SmartUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Interfaces
{
    public interface IAssignmentRepository
    {
        Task AddAsync(Assignment assignment);

        Task<List<Assignment>> GetByCourseIdAsync(int courseId);
        Task<Assignment?> GetByIdAsync(int id);

    }
}
