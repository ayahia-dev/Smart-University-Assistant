using Microsoft.EntityFrameworkCore;
using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;
using SmartUniversity.Infrastructure.Persistence;

public class AssignmentRepository : IAssignmentRepository
{
    private readonly ApplicationDbContext _context;

    public AssignmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Assignment assignment)
    {
        await _context.Assignments.AddAsync(assignment);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Assignment>> GetByCourseIdAsync(int courseId)
    {
        return await _context.Assignments
            .Where(x => x.CourseId == courseId)
            .ToListAsync();
    }
    public async Task<Assignment?> GetByIdAsync(int id)
    {
        return await _context.Assignments
            .FirstOrDefaultAsync(x => x.Id == id);
    }

}