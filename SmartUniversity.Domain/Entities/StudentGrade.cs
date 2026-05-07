using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Entities
{
    public class StudentGrade
    {
        public int Id { get; private set; }

        public int StudentId { get; private set; }

        public User Student { get; private set; }

        public int CourseId { get; private set; }

        public Course Course { get; private set; }

        public double MidtermGrade { get; private set; }

        public double FinalGrade { get; private set; }

        public bool IsPublished { get; private set; }

        private StudentGrade() { }

        public StudentGrade(int studentId, int courseId)
        {
            StudentId = studentId;
            CourseId = courseId;
        }

        public void Update(double mid, double final)
        {
            MidtermGrade = mid;
            FinalGrade = final;
        }

        public void Publish()
        {
            IsPublished = true;
        }
        public double GetTotalGrade()
        {
            return MidtermGrade + FinalGrade;
        }
        public void SetMidterm(double mid)
        {
            if (mid < 0)
                throw new Exception("Invalid midterm");

            MidtermGrade = mid;
        }

        public void SetFinal(double final)
        {
            if (final < 0)
                throw new Exception("Invalid final");

            FinalGrade = final;
        }
    }
}
