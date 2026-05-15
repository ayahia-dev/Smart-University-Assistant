using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using SmartUniversity.Domain.Entities;
    using SmartUniversity.Domain.Interfaces;
    using SmartUniversity.Infrastructure.Persistence;

    public class AssignmentSubmissionRepository : IAssignmentSubmissionRepository
    {
        private readonly ApplicationDbContext _context;

        public AssignmentSubmissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AssignmentSubmission submission)
        {
            await _context.AssignmentSubmissions.AddAsync(submission);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int assignmentId, int studentId)
        {
            return await _context.AssignmentSubmissions
                .AnyAsync(x => x.AssignmentId == assignmentId && x.StudentId == studentId);
        }

        public async Task<List<AssignmentSubmission>> GetByAssignmentIdAsync(int assignmentId)
        {
            return await _context.AssignmentSubmissions
                .Include(x => x.Student)
                .Where(x => x.AssignmentId == assignmentId)
                .ToListAsync();
        }

        public async Task<AssignmentSubmission?> GetByIdAsync(int id)
        {
            return await _context.AssignmentSubmissions.FindAsync(id);
        }

        public async Task UpdateAsync(AssignmentSubmission submission)
        {
            _context.AssignmentSubmissions.Update(submission);
            await _context.SaveChangesAsync();
        }
        public async Task<List<double>> GetStudentAssignmentScores(
    int studentId,
    int courseId
)
        {
            return await _context.AssignmentSubmissions

                .Include(x => x.Assignment)

                .Where(x =>
                    x.StudentId == studentId &&
                    x.Assignment.CourseId == courseId
                )

                .Select(x =>

                    x.Assignment.MaxGrade == 0
                    ? 0
                    : ((x.Grade ?? 0) / x.Assignment.MaxGrade) * 100
                )

                .ToListAsync();
        }
        public async Task<bool> HasSubmissionsAsync(int assignmentId)
        {
            return await _context.AssignmentSubmissions
                .AnyAsync(x => x.AssignmentId == assignmentId);
        }
    }
}
