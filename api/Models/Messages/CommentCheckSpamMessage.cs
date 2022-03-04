using System;

namespace RoboKiwi.Functions.Models.Messages;

public class CommentCheckSpamMessage
{
    /// <summary>
    /// Message id, which also acts as the id for the generated comment.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// The id of the page this comment is for
    /// </summary>
    public Guid PageId { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }
    
    public string Subject { get; set; }

    public string Content { get; set; }

    public string UserIp { get; set; }

    public DateTime Date { get; set; }

    public Uri Url { get; set; }

    public Uri Referrer { get; set; }

    public string ReplyTo { get; set; }

    public string Author { get; set; }

    public string PageDate { get; set; }
}