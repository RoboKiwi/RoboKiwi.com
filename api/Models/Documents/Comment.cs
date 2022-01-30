using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace RoboKiwi.Functions.Models.Documents;

public class Comment
{
    /// <summary>
    /// Id
    /// </summary>
    [JsonPropertyName("id")]
    [JsonProperty("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("t")] 
    [JsonProperty("t")]
    public EntityType Type { get; } = EntityType.Comment;

    /// <summary>
    /// The id of the <see cref="Page"/> this comment belongs to, and the partition key for comments.
    /// </summary>
    /// <remarks>Partition key</remarks>
    [JsonPropertyName("pid")]
    [JsonProperty("pid")]
    public Guid Pid { get; set; }

    /// <summary>
    /// Id of the parent comment this is in reply to, if any.
    /// </summary>
    [JsonPropertyName("replyTo")]
    [JsonProperty("replyTo")]
    public Guid? ReplyTo { get; set; }

    [JsonPropertyName("email")]
    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonPropertyName("name")]
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonPropertyName("content")]
    [JsonProperty("content")]
    public string Content { get; set; }

    [JsonPropertyName("userIp")]
    [JsonProperty("userIp")]
    public string UserIp { get; set; }

    /// <summary>
    /// The date and time this comment was posted.
    /// </summary>
    [JsonPropertyName("date")]
    [JsonProperty("date")]
    public DateTime Date { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// The url of the page the comment was originally posted to.
    /// </summary>
    [JsonPropertyName("url")]
    [JsonProperty("url")]
    public Uri Url { get; set; }

    /// <summary>
    /// Whether it has been marked as spam or ham
    /// </summary>
    [JsonPropertyName("spam")]
    [JsonProperty("spam")]
    public SpamClassification Spam { get; set; }
}