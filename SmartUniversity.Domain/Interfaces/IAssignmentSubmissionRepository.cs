using SmartUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Interfaces
{
    public interface IAssignmentSubmissionRepository
    {
        Task AddAsync(AssignmentSubmission submission);

        Task<bool> ExistsAsync(int assignmentId, int studentId);

        Task<List<AssignmentSubmission>> GetByAssignmentIdAsync(int assignmentId);

        Task<AssignmentSubmission?> GetByIdAsync(int id);

        Task UpdateAsync(AssignmentSubmission submission);
        Task<List<double>> GetStudentAssignmentScores(
            int studentId,
            int courseId
        );
        Task<bool> HasSubmissionsAsync(int assignmentId);
    }
}
