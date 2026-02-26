using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Entities
{
 
    public class Assignment
    {
        public int Id { get; private set; }

        public int CourseId { get; private set; }
        public Course Course { get; private set; }

        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;

        public DateTime DueDate { get; private set; }
        public double MaxGrade { get; private set; }

        private Assignment() { } // For EF

        public Assignment(int courseId, string title, string description, DateTime dueDate, double maxGrade)
        {
            if (maxGrade <= 0)
                throw new ArgumentException("Max grade must be greater than zero.");

            CourseId = courseId;
            Title = title;
            Description = description;
            DueDate = dueDate;
            MaxGrade = maxGrade;
        }

        public void UpdateDetails(string title, string description, DateTime dueDate)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
        }

        public void ChangeMaxGrade(double maxGrade)
        {
            if (maxGrade <= 0)
                throw new ArgumentException("Invalid max grade.");

            MaxGrade = maxGrade;
        }
    }

}
