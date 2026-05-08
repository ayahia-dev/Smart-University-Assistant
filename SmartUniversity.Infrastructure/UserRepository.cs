using Microsoft.EntityFrameworkCore;
using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;
using SmartUniversity.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetPagedAsync(string? name, string? role, int page, int pageSize)
    {
        var query = _context.Users
        .Include(x => x.Faculty) 
        .AsQueryable();

        if (!string.IsNullOrEmpty(name))
            query = query.Where(x => x.FullName.Contains(name));

        if (!string.IsNullOrEmpty(role))
            query = query.Where(x => x.Role == role);

        return await query
            .OrderByDescending(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountAsync(string? name, string? role)
    {
        var query = _context.Users
        .Include(x => x.Faculty) 
        .AsQueryable();

        if (!string.IsNullOrEmpty(name))
            query = query.Where(x => x.FullName.Contains(name));

        if (!string.IsNullOrEmpty(role))
            query = query.Where(x => x.Role == role);

        return await query.CountAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users

            .Include(x => x.Faculty)

            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Users.AnyAsync(x => x.Email == email);
    }
    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
    public async Task<bool> HasCoursesAsync(int doctorId)
    {
        return await _context.Courses
            .AnyAsync(c => c.DoctorId == doctorId);
    }
    public async Task<List<User>> GetStudentsByFacultyAsync(int facultyId)
    {
        return await _context.Users
            .Include(x => x.Faculty)
            .Include(x => x.StudentGrades)
            .Where(x =>
                x.Role == "Student" &&
                x.FacultyId == facultyId)
            .ToListAsync();
    }
}