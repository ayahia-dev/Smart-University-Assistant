using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Entities
{
    public class Faculty
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public ICollection<User> Users { get; private set; }
            = new List<User>();

        public ICollection<Course> Courses { get; private set; }
            = new List<Course>();

        private Faculty() { }

        public Faculty(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Faculty(string name)
        {
            Name = name;
        }
        public void UpdateName(string name)
        {
            Name = name;
        }
    }
}
