﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.Service
{
    //public interface EmailService 
    //{
    //    Task SendEmailsync(string toemail, string subject, string content);

    //}

    //public class SendGridEmailService : EmailService
    //{
    //    public Task SendEmailsync(string toemail, string subject, string content)
    //    {
    //        var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
    //        var client = new SendGridClient(apiKey);
    //        var from = new EmailAddress("test@example.com", "Example User");
    //        var subject = "Sending with SendGrid is Fun";
    //        var to = new EmailAddress("test@example.com", "Example User");
    //        var plainTextContent = "and easy to do anywhere, even with C#";
    //        var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
    //        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
    //        var response = await client.SendEmailAsync(msg);
    //    }
    //}
}
