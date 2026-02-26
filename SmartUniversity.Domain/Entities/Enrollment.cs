using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Entities
{
    public class Enrollment
    {
        public int Id { get; private set; }

        public int StudentId { get; private set; }
        public User Student { get; private set; }

        public int CourseId { get; private set; }
        public Course Course { get; private set; }

        public DateTime EnrolledAt { get; private set; }

        private Enrollment() { }

        public Enrollment(int studentId, int courseId)
        {
            StudentId = studentId;
            CourseId = courseId;
            EnrolledAt = DateTime.UtcNow;
        }
    }
}
