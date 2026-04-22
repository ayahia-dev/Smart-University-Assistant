using SmartUniversity.Application.DTOs.Assignment;
using SmartUniversity.Application.Interfaces;
using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.Services
{
    public class AssignmentSubmissionService : IAssignmentSubmissionService
    {
        private readonly IAssignmentSubmissionRepository _repo;

        public AssignmentSubmissionService(IAssignmentSubmissionRepository repo)
        {
            _repo = repo;
        }

        public async Task SubmitAsync(int studentId, SubmitDto dto)
        {
            if (await _repo.ExistsAsync(dto.AssignmentId, studentId))
                throw new Exception("Already submitted");

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.File.FileName);

            var filePath = Path.Combine("wwwroot/uploads", fileName);

            Directory.CreateDirectory("wwwroot/uploads");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            var fileUrl = $"/uploads/{fileName}";

            var submission = new AssignmentSubmission(
                dto.AssignmentId,
                studentId,
                fileUrl
            );

            await _repo.AddAsync(submission);
        }

        public async Task<IEnumerable<SubmissionDto>> GetByAssignmentAsync(int assignmentId)
        {
            var submissions = await _repo.GetByAssignmentIdAsync(assignmentId);

            return submissions.Select(x => new SubmissionDto
            {
                Id = x.Id,
                StudentName = x.Student.FullName,
                FileUrl = x.FileUrl,
                SubmittedAt = x.SubmittedAt,
                Grade = x.Grade,
                Feedback = x.Feedback
            });
        }

        public async Task GradeAsync(int submissionId, GradeDto dto)
        {
            var submission = await _repo.GetByIdAsync(submissionId);

            if (submission == null)
                throw new Exception("Submission not found");

            submission.GradeSubmission(dto.Grade, dto.Feedback);

            await _repo.UpdateAsync(submission);
        }
    }
}
