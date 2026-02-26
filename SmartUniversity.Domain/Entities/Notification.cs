using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Entities;

public class Notification
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Body { get; private set; } = string.Empty;
    public bool IsRead { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Notification() { }

    public Notification(int userId, string title, string body)
    {
        UserId = userId;
        Title = title;
        Body = body;
        CreatedAt = DateTime.UtcNow;
        IsRead = false;
    }

    public void MarkAsRead()
    {
        IsRead = true;
    }
}
