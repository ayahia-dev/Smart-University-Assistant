using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Entities
{
    public class Course
    {
        public int Id { get; private set; }

        public string Code { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;

        public int FacultyId { get; private set; }
        public Faculty Faculty { get; private set; }

        public int DoctorId { get; private set; }
        public User Doctor { get; private set; }
        public double MaxMidterm { get; private set; }
        public double MaxFinal { get; private set; }

        public ICollection<Enrollment> Enrollments { get; private set; }
            = new List<Enrollment>();

        private Course() { }

        public Course(
                string code,
                string name,
                string description,
                int doctorId,
                int facultyId)
        {
            Code = code;
            Name = name;
            Description = description;
            DoctorId = doctorId;
            FacultyId = facultyId;
        }

        public void UpdateDetails(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public void SetGradingScheme(double maxMid, double maxFinal)
        {
            if (maxMid <= 0 || maxFinal <= 0)
                throw new Exception("Invalid max grades");

            MaxMidterm = maxMid;
            MaxFinal = maxFinal;
        }

    }
}
