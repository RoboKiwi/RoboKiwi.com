using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace RoboKiwi.Functions
{
    public static class PostComment
    {
        internal static string GetAppSetting(string name)
        {
            return Environment.GetEnvironmentVariable(name);
        }

        internal static string GetAkismetApiKey() => GetAppSetting("AkismetApiKey");

        [FunctionName("PostComment")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest request, 
            ILogger log)
        {
            log.LogInformation("A comment was posted.");
            
            var headers = request.GetTypedHeaders();
            var form = await request.ReadFormAsync();

            var model = new CommentPost();

            model.Name = form["name"];
            model.Email = form["email"];
            model.Comment = form["comment"];
            model.Subject = form["subject"];

            model.Referrer = headers.Referer;
            model.UserIp = request.GetClientIp();
            model.Url = headers.Referer?.ToString();
            model.Permalink = headers.Referer?.ToString();
            model.Date = DateTime.UtcNow;



            var result = bool.Parse( await AkismetApiClient.CommentCheck(model) );

            model.IsSpam = result;

            return new OkObjectResult(result);
        }
    }
}
