using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using RoboKiwi.Functions.Helpers;
using RoboKiwi.Functions.Models;
using RoboKiwi.Functions.Models.Messages;
using ServiceBusEntityType = Microsoft.Azure.WebJobs.ServiceBus.EntityType;

namespace RoboKiwi.Functions;

public static class PostCommentHttpTrigger
{
    internal static string GetAppSetting(string name)
    {
        return Environment.GetEnvironmentVariable(name);
    }

    internal static string GetAkismetApiKey() => GetAppSetting("AkismetApiKey");

    [FunctionName("PostComment")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest request,
        [ServiceBus("CommentPosted", EntityType = ServiceBusEntityType.Queue, Connection = "ServiceBus")] IAsyncCollector<CommentCheckSpamMessage> messages,
        ILogger log)
    {
        log.LogInformation("A comment was posted.");
            
        var headers = request.GetTypedHeaders();
        var form = await request.ReadFormAsync();

        var model = new CommentPost();

        model.ReplyTo = form["replyto"];
        model.Name = form["name"];
        model.Email = form["email"];
        model.Content = form["comment"];
        model.Subject = form["subject"];
        model.Url = new Uri(form["url"], UriKind.Relative);
        model.PageId = Guid.Parse(form["guid"]);
        model.Author = form["author"];
        model.PageDate = form["date"];
        model.Hmac = form["hmac"];

        model.Referrer = headers.Referer;
        model.UserIp = request.GetClientIp();
        model.Date = DateTime.UtcNow;
        
        // If the honeypot field was touched, we should reject this as spam
        if (!string.IsNullOrEmpty(model.Subject)) return new BadRequestResult();

        // Verify the HMAC
        // GUID-Url-Date-Author
        var sb = new StringBuilder()
            .Append(model.PageId)
            .Append(model.Referrer.AbsolutePath)
            .Append(model.PageDate)
            .Append(model.Author);

        if (!await CryptoHelper.VerifyHmacSha512(Environment.GetEnvironmentVariable("HmacPrivateKey"), sb.ToString(), model.Hmac))
        {
            // Failed validation
            return new BadRequestResult();
        }

        // Now post a message to get the comment checked for spam
        var message = new CommentCheckSpamMessage
        {
            Id = Guid.NewGuid(),
            PageId = model.PageId,
            Date = model.Date,
            Url = model.Url,
            Content = model.Content,
            Author = model.Author,
            Email = model.Email,
            Name = model.Name,
            UserIp = model.UserIp,
            PageDate = model.PageDate,
            Referrer = model.Referrer,
            ReplyTo = model.ReplyTo,
            Subject = model.Subject
        };
        await messages.AddAsync(message);

        return new RedirectResult(model.Url.ToString(), false, false);
    }
}