using System;

namespace RoboKiwi.Functions.Models;

public class CommentPost
{
    public string Email { get; set; }
    
    public string Name { get; set; }

    public Uri Url { get; set; }

    public string Subject { get; set; }

    public string Content { get; set; }

    public string UserIp { get; set; }

    public DateTime Date { get; set; }
    
    public Uri Referrer { get; set; }
    
    public string ReplyTo { get; set; }
    
    public Guid PageId { get; set; }

    public string Hmac { get; set; }

    public string Author { get; set; }

    public string PageDate { get; set; }
}