using SmartUniversity.Application.DTOs.Assignment;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.Interfaces
{
    public interface IAssignmentSubmissionService
    {
        Task SubmitAsync(int studentId, SubmitDto dto);

        Task<IEnumerable<SubmissionDto>> GetByAssignmentAsync(int assignmentId);

        Task GradeAsync(int submissionId, GradeDto dto);
    }
}
