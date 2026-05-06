using Microsoft.EntityFrameworkCore;
using SmartUniversity.Application.DTOs.Lecture;
using SmartUniversity.Application.Interfaces;
using SmartUniversity.Infrastructure.Persistence;

namespace SmartUniversity.Infrastructure
{
    public class LectureQuery : ILectureQuery
    {
        private readonly ApplicationDbContext _context;

        public LectureQuery(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LectureDto>> GetDoctorLecturesAsync(int doctorId)
        {
            return await _context.Lectures
                .Where(l => l.Course.DoctorId == doctorId)
                .Select(l => new LectureDto
                {
                    Id = l.Id,
                    Title = l.Title,
                    Subtitle = l.Subtitle,
                    Room = l.Room,
                    Day = l.Day,
                    StartTime = l.StartTime,
                    EndTime = l.EndTime
                })
                .ToListAsync();
        }
    }
}