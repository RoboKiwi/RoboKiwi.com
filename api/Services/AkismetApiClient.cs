using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using RoboKiwi.Functions.Helpers;
using RoboKiwi.Functions.Models.Messages;

namespace RoboKiwi.Functions.Services;

static class AkismetApiClient
{
    static readonly HttpClient client = new();

    public static bool CommentCheck(CommentCheckSpamMessage comment)
    {
        var form = new Dictionary<string, string>
        {
            {"blog", "https://www.robokiwi.com/"},
            {"user_ip", comment.UserIp},
            {"referrer", comment.Referrer.ToString()},
            {"permalink", comment.Url.ToString()},
            {"comment_type","comment"},
            {"comment_author",comment.Name},
            {"comment_author_email",comment.Email},
            //{"comment_author_url",comment.Url},
            {"comment_content", comment.Content},
            {"comment_date_gmt", comment.Date.ToISO8601()},
            {"blog_lang","en"},
            {"blog_charset","utf-8"},
            {"is_test","true"},
            // {"honeypot_field_name ","subject"},
            // {"subject ", comment.Subject},

            // {"user_role","administrator"},
            // {"recheck_reason","edit"},
        };

        if (DateTimeOffset.TryParse(comment.PageDate, out DateTimeOffset pageEdited))
        {
            form.Add("comment_post_modified_gmt", pageEdited.UtcDateTime.ToISO8601());
        }
        
        var apiKey = PostCommentHttpTrigger.GetAkismetApiKey();

        var url = $"https://{apiKey}.rest.akismet.com/1.1/comment-check";

        using var request = new HttpRequestMessage(HttpMethod.Post, url);

        var content = new FormUrlEncodedContent(form);
        request.Content = content;

        request.Headers.Add("User-Agent", "WordPress/4.4.1 | Akismet/3.1.7");

        
        var response = client.Send(request);

        using var reader = new StreamReader(response.Content.ReadAsStream());

        var result = reader.ReadToEnd();
        
        return bool.Parse(result);

        // X-akismet-alert-code
        // X-akismet-alert-msg
        // X-akismet-guid

        // Response Error Codes
        //
        //     An API response may include an error code in the X-akismet-alert-code header.It is always accompanied by a X-akismet-alert-msg header containing a message describing the error that can be shown to the end user, but the list below includes all of the currently active error codes for reference.
        //
        // 10001: Your site is using an expired Yahoo! Small Business API key.
        // 10003: You must upgrade your Personal subscription to continue using Akismet.
        // 10005: Your Akismet API key may be in use by someone else.
        // 10006: Your subscription has been suspended due to improper use.
        // 10007: Your Akismet API key is being used on more sites than is allowed.
        // 10008: Your Akismet API key is being used on more sites than is allowed.
        // 10009: Your subscription has been suspended due to overuse.
        // 10010: Your subscription has been suspended due to inappropriate use.
        // 10011: Your subscription needs to be upgraded due to high usage.
        // 10402: Your API key was suspended for non-payment.
        // 10403: The owner of your API key has revoked your site's access to the key.
        // 10404: Your site was not found in the list of sites allowed to use the API key you used.
        // 30001: Your Personal subscription needs to be upgraded based on your usage.
    }

}