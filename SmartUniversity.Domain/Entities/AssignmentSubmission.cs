using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Entities;

public class AssignmentSubmission
{
    public int Id { get; private set; }

    public int AssignmentId { get; private set; }
    public Assignment Assignment { get; private set; }

    public int StudentId { get; private set; }
    public User Student { get; private set; }

    public string FileUrl { get; private set; } = string.Empty;

    public DateTime SubmittedAt { get; private set; }

    public double? Grade { get; private set; }
    public string? Feedback { get; private set; }

    private AssignmentSubmission() { }

    public AssignmentSubmission(
    int assignmentId,
    int studentId,
    string fileUrl)
    {
        AssignmentId = assignmentId;

        StudentId = studentId;

        FileUrl = fileUrl;

        SubmittedAt = DateTime.UtcNow;
    }

    public void GradeSubmission(double grade, string? feedback = null)
    {
        if (grade < 0)
            throw new ArgumentException("Invalid grade.");

        Grade = grade;
        Feedback = feedback;
    }
}
