using SmartUniversity.Domain.Entities;
using SmartUniversity.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Application.Services
{
    public class AttendanceService
    {
        private readonly IAttendanceRepository _repo;

        public AttendanceService(IAttendanceRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> CreateSession(int courseId)
        {
            var session = new AttendanceSession(courseId, DateTime.UtcNow);

            var created = await _repo.CreateSessionAsync(session);

            return created.Id;
        }

        public async Task AddAttendance(int sessionId, List<(int studentId, bool isPresent)> students)
        {
            var records = students.Select(x =>
                new AttendanceRecord(sessionId, x.studentId, x.isPresent)
            ).ToList();

            await _repo.AddRecordsAsync(records);
        }
    }
}
