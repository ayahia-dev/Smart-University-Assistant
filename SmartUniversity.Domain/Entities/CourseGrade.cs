using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Entities;

public class CourseGrade
{
    public int Id { get; private set; }
    public int StudentId { get; private set; }
    public int CourseId { get; private set; }

    public double AssignmentAverage { get; private set; }
    public double QuizAverage { get; private set; }
    public double AttendancePercentage { get; private set; }
    public double FinalGrade { get; private set; }

    private CourseGrade() { }

    public CourseGrade(
        int studentId,
        int courseId,
        double assignmentAverage,
        double quizAverage,
        double attendancePercentage)
    {
        StudentId = studentId;
        CourseId = courseId;
        AssignmentAverage = assignmentAverage;
        QuizAverage = quizAverage;
        AttendancePercentage = attendancePercentage;

        FinalGrade = CalculateFinalGrade();
    }

    private double CalculateFinalGrade()
    {
        return (AssignmentAverage * 0.3)
             + (QuizAverage * 0.4)
             + (AttendancePercentage * 0.3);
    }
}
