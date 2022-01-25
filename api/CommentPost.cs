using System;

namespace RoboKiwi.Functions;

internal class CommentPost
{
    public string Email { get; set; }

    public string Name { get; set; }

    public string Url { get; set; }

    public string Subject { get; set; }

    public string Comment { get; set; }

    public string UserIp { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;

    public DateTime? PostModifiedDate { get; set; }

    public string Permalink { get; set; }

    public Uri Referrer { get; set; }

    public bool IsSpam { get; set; }
}