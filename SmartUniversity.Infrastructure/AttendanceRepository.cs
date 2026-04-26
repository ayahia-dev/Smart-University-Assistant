using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;
using SmartUniversity.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace SmartUniversity.Infrastructure
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AttendanceSession> CreateSessionAsync(AttendanceSession session)
        {
            await _context.AttendanceSessions.AddAsync(session);
            await _context.SaveChangesAsync();
            return session;
        }

        public async Task AddRecordsAsync(List<AttendanceRecord> records)
        {
            await _context.AttendanceRecords.AddRangeAsync(records);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AttendanceSession>> GetSessionsByCourseAsync(int courseId)
        {
            return await _context.AttendanceSessions
                .Where(x => x.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<List<AttendanceRecord>> GetRecordsBySessionAsync(int sessionId)
        {
            return await _context.AttendanceRecords
                .Where(x => x.SessionId == sessionId)
                .ToListAsync();
        }

        public async Task<double> GetAttendancePercentage(int studentId, int courseId)
        {
            var sessions = await _context.AttendanceSessions
                .Where(x => x.CourseId == courseId)
                .Select(x => x.Id)
                .ToListAsync();

            if (!sessions.Any())
                return 0;

            var total = sessions.Count;

            var present = await _context.AttendanceRecords
                .CountAsync(x =>
                    sessions.Contains(x.SessionId) &&
                    x.StudentId == studentId &&
                    x.IsPresent
                );

            return (double)present / total * 100;
        }
    }
    }