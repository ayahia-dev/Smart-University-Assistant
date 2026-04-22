using SmartUniversity.Application.DTOs.Assignment;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.Interfaces
{
    public interface IAssignmentService
    {
        Task CreateAsync(CreateAssignmentDto dto);

        Task<IEnumerable<AssignmentDto>> GetByCourseAsync(int courseId);
    }
}
