using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Entities
{
    public class News
    {
        public int Id { get; private set; }

        public string Title { get; private set; } = string.Empty;
        public string Content { get; private set; } = string.Empty;

        public DateTime CreatedAt { get; private set; }

        private News() { }

        public News(string title, string content)
        {
            Title = title;
            Content = content;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }
}
