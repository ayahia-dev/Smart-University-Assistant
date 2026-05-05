using Microsoft.EntityFrameworkCore;
using SmartUniversity.Application.DTOs.Student;
using SmartUniversity.Application.Interfaces;
using SmartUniversity.Infrastructure.Persistence;

public class CourseStudentQuery : ICourseStudentQuery
{
    private readonly ApplicationDbContext _context;

    public CourseStudentQuery(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<StudentDto>> GetStudentsByCourseIdAsync(int courseId)
    {
        return await _context.Enrollments
            .Where(e => e.CourseId == courseId)
            .Select(e => new StudentDto
            {
                Id = e.Student.Id,
                FullName = e.Student.FullName
            })
            .ToListAsync();
    }
}