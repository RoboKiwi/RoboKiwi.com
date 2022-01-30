using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using RoboKiwi.Functions.Models.Documents;
using RoboKiwi.Functions.Models.Messages;
using RoboKiwi.Functions.Services;

namespace RoboKiwi.Functions
{
    public static class CommentPostedQueueTrigger 
    {
        [FunctionName("CommentCheckSpam")]
        public static void Run(
            [ServiceBusTrigger("CommentPosted", Connection = "ServiceBus")]CommentCheckSpamMessage message,
            [CosmosDB("robokiwi", "comments", Connection = "DocumentDatabase", Id = "{Id}", PartitionKey = "{PageId}", CreateIfNotExists = true)]out Comment comment,
            ILogger log)
        {
            log.LogInformation($"Spam check for comment {message.Id} on page {message.PageId}");

            log.LogInformation("Checking comment against Akismet API");

            comment = new Comment();
            comment.Id = message.Id;
            comment.Pid = message.PageId;
            comment.Content = message.Content;
            comment.Date = message.Date;
            comment.Email = message.Email;
            comment.Pid = message.PageId;
            comment.Name = message.Name;
            comment.UserIp = message.UserIp;
            comment.Url = message.Url;
            comment.ReplyTo = string.IsNullOrWhiteSpace(message.ReplyTo) ? null : Guid.Parse(message.ReplyTo);
            
            var result = AkismetApiClient.CommentCheck(message);

            comment.Spam = result ? SpamClassification.ApiMarkedSpam : SpamClassification.ApiMarkedHam;
        }
    }
}
