using SmartUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<AttendanceSession> CreateSessionAsync(AttendanceSession session);
        Task AddRecordsAsync(List<AttendanceRecord> records);

        Task<List<AttendanceSession>> GetSessionsByCourseAsync(int courseId);
        Task<List<AttendanceRecord>> GetRecordsBySessionAsync(int sessionId);

        Task<double> GetAttendancePercentage(int studentId, int courseId);
    }
}
