using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Entities;

public class Quiz
{
    public int Id { get; private set; }

    public int CourseId { get; private set; }
    public Course Course { get; private set; }

    public string Title { get; private set; } = string.Empty;
    public double TotalGrade { get; private set; }

    public DateTime QuizDate { get; private set; }
    public int DurationMinutes { get; private set; }
    public ICollection<Question> Questions { get; private set; }
    = new List<Question>();
    private Quiz() { }

    public Quiz(int courseId, string title, double totalGrade, DateTime quizDate, int durationMinutes)
    {
        if (totalGrade <= 0)
            throw new ArgumentException("Total grade must be greater than zero.");

        if (durationMinutes <= 0)
            throw new ArgumentException("Invalid duration.");

        CourseId = courseId;
        Title = title;
        TotalGrade = totalGrade;
        QuizDate = quizDate;
        DurationMinutes = durationMinutes;
    }

    public void UpdateDetails(string title, DateTime quizDate, int durationMinutes)
    {
        Title = title;
        QuizDate = quizDate;
        DurationMinutes = durationMinutes;
    }
    public DateTime GetEndTime()
    {
        return QuizDate.AddMinutes(DurationMinutes);
    }

    public bool HasStarted(DateTime now)
    {
        return now >= QuizDate;
    }

    public bool IsFinished(DateTime now)
    {
        return now > GetEndTime();
    }

    public TimeSpan GetRemainingTime(DateTime now)
    {
        var remaining = GetEndTime() - now;

        return remaining.TotalSeconds > 0 ? remaining : TimeSpan.Zero;
    }

}


