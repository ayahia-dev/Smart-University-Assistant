using Microsoft.EntityFrameworkCore;
using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;
using SmartUniversity.Infrastructure.Persistence;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly ApplicationDbContext _context;

    public EnrollmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Enrollment enrollment)
    {
        await _context.Enrollments.AddAsync(enrollment);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int studentId, int courseId)
    {
        return await _context.Enrollments
            .AnyAsync(x => x.StudentId == studentId && x.CourseId == courseId);
    }

    public async Task<List<Enrollment>> GetByStudentIdAsync(int studentId)
    {
        return await _context.Enrollments

            .Include(x => x.Course)

            .Include(x => x.Student)

            .Where(x => x.StudentId == studentId)

            .ToListAsync();
    }

    public async Task<List<Enrollment>> GetByCourseIdAsync(int courseId)
    {
        return await _context.Enrollments
            .Include(x => x.Student)
            .Where(x => x.CourseId == courseId)
            .ToListAsync();
    }
    public async Task<List<User>> GetStudentsByCourseIdAsync(int courseId)
    {
        return await _context.Enrollments
            .Where(e => e.CourseId == courseId)
            .Select(e => e.Student)
            .ToListAsync();
    }
    public async Task<Enrollment?> GetAsync(
    int studentId,
    int courseId)
    {
        return await _context.Enrollments
            .FirstOrDefaultAsync(x =>
                x.StudentId == studentId &&
                x.CourseId == courseId);
    }

    public async Task DeleteAsync(Enrollment enrollment)
    {
        _context.Enrollments.Remove(enrollment);

        await _context.SaveChangesAsync();
    }
}