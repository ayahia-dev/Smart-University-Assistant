using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Entities;

public class QuizSubmission
{
    public int Id { get; private set; }

    public int QuizId { get; private set; }
    public Quiz Quiz { get; private set; }

    public int StudentId { get; private set; }
    public User Student { get; private set; }

    public double Score { get; private set; }

    public DateTime SubmittedAt { get; private set; }
    public DateTime StartTime { get; private set; }

    private QuizSubmission() { }

    public QuizSubmission(int quizId, int studentId, double score)
    {
        QuizId = quizId;
        StudentId = studentId;
        Score = score;
        SubmittedAt = DateTime.UtcNow;
        StartTime = DateTime.UtcNow; 
    }
    public void SetScore(double score)
    {
        Score = score;
    }
}
