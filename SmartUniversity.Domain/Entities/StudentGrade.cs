using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Entities
{
    public class StudentGrade
    {
        public int Id { get; private set; }

        public int StudentId { get; private set; }
        public int CourseId { get; private set; }

        public double MidtermGrade { get; private set; }
        public double FinalGrade { get; private set; }

        public bool IsPublished { get; private set; } // 🔥

        private StudentGrade() { }

        public StudentGrade(int studentId, int courseId,
            double mid, double final)
        {
            StudentId = studentId;
            CourseId = courseId;
            MidtermGrade = mid;
            FinalGrade = final;
            IsPublished = false;
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
    }
}
