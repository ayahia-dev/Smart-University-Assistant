using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Entities;

public class AttendanceRecord
{
    public int Id { get; private set; }

    public int SessionId { get; private set; }
    public AttendanceSession Session { get; private set; }

    public int StudentId { get; private set; }
    public User Student { get; private set; }

    public bool IsPresent { get; private set; }

    private AttendanceRecord() { }

    public AttendanceRecord(int sessionId, int studentId, bool isPresent)
    {
        SessionId = sessionId;
        StudentId = studentId;
        IsPresent = isPresent;
    }

    public void MarkPresent()
    {
        IsPresent = true;
    }

    public void MarkAbsent()
    {
        IsPresent = false;
    }
}
