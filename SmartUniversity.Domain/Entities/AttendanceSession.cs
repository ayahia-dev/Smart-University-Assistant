using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Entities;

public class AttendanceSession
{
    public int Id { get; private set; }

    public int CourseId { get; private set; }
    public Course Course { get; private set; }

    public DateTime SessionDate { get; private set; }

    public ICollection<AttendanceRecord> Records { get; private set; }
        = new List<AttendanceRecord>();

    private AttendanceSession() { }

    public AttendanceSession(int courseId, DateTime sessionDate)
    {
        CourseId = courseId;
        SessionDate = sessionDate;
    }
}
