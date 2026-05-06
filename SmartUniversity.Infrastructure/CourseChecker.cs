using Microsoft.EntityFrameworkCore;
using SmartUniversity.Domain.Interfaces;
using SmartUniversity.Infrastructure.Persistence;

namespace SmartUniversity.Infrastructure
{
    public class CourseChecker : ICourseChecker
    {
        private readonly ApplicationDbContext _context;

        public CourseChecker(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> HasEnrollmentsAsync(int courseId)
        {
            return await _context.Enrollments
                .AnyAsync(x => x.CourseId == courseId);
        }
    }
}