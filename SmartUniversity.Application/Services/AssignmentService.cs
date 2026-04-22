using SmartUniversity.Application.DTOs.Assignment;
using SmartUniversity.Application.Interfaces;
using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _repo;

        public AssignmentService(IAssignmentRepository repo)
        {
            _repo = repo;
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

            await _repo.AddAsync(assignment);
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
    }
}
