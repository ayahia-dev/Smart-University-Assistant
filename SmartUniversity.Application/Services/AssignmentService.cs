using SmartUniversity.Application.DTOs.Assignment;
using SmartUniversity.Application.Exceptions;
using SmartUniversity.Application.Interfaces;
using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;

namespace SmartUniversity.Application.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _repo;
        private readonly IGenericRepository<Assignment> _genericRepo; 
        private readonly IAssignmentSubmissionRepository _submissionRepo; 

        public AssignmentService(
            IAssignmentRepository repo,
            IGenericRepository<Assignment> genericRepo,
            IAssignmentSubmissionRepository submissionRepo)
        {
            _repo = repo;
            _genericRepo = genericRepo;
            _submissionRepo = submissionRepo;
        }

        public async Task CreateAsync(CreateAssignmentDto dto)
        {
            var assignment = new Assignment(
                dto.CourseId,
                dto.Title,
                dto.Description,
                dto.DueDate,
                dto.MaxGrade
            );

            await _genericRepo.AddAsync(assignment);
            await _genericRepo.SaveChangesAsync(); 
        }

        public async Task<IEnumerable<AssignmentDto>> GetByCourseAsync(int courseId)
        {
            var assignments = await _repo.GetByCourseIdAsync(courseId);

            return assignments.Select(x => new AssignmentDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                DueDate = x.DueDate,
                MaxGrade = x.MaxGrade
            });
        }

        public async Task DeleteAsync(int id)
        {
            var assignment = await _genericRepo.GetByIdAsync(id);

            if (assignment == null)
                throw new BadRequestException("Assignment not found");

            if (await _submissionRepo.HasSubmissionsAsync(id))
                throw new BadRequestException("Cannot delete assignment with submissions");

            _genericRepo.Delete(assignment);
            await _genericRepo.SaveChangesAsync();
        }
    }
}