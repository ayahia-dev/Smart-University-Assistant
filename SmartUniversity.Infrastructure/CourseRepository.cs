using Microsoft.EntityFrameworkCore;
using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;
using SmartUniversity.Infrastructure.Persistence;

public class CourseRepository : ICourseRepository
{
    private readonly ApplicationDbContext _context;

    public CourseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Course>> GetPagedAsync(string? name, int page, int pageSize)
    {
        var query = _context.Courses
         .Include(x => x.Doctor)
         .Include(x => x.Faculty)
         .AsQueryable();

        if (!string.IsNullOrEmpty(name))
            query = query.Where(x => x.Name.Contains(name));

        return await query
            .OrderByDescending(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountAsync(string? name)
    {
        var query = _context.Courses.AsQueryable();

        if (!string.IsNullOrEmpty(name))
            query = query.Where(x => x.Name.Contains(name));

        return await query.CountAsync();
    }

    public async Task<Course?> GetByIdAsync(int id)
    {
        return await _context.Courses.FindAsync(id);
    }

    public async Task AddAsync(Course course)
    {
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByCodeAsync(string code)
    {
        return await _context.Courses.AnyAsync(x => x.Code == code);
    }
    public async Task UpdateAsync(Course course)
    {
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
    }
    public async Task<List<Course>> GetByDoctorIdAsync(int doctorId)
    {
        return await _context.Courses
            .Where(c => c.DoctorId == doctorId)
            .ToListAsync();
    }
    public async Task<List<Course>> GetByFacultyIdAsync(int facultyId)
    {
        return await _context.Courses
            .Where(x => x.FacultyId == facultyId)
            .ToListAsync();
    }
}