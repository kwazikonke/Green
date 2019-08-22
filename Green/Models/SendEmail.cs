﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;


namespace Green.Models
{
    public class SendEmail
    {
       
        private static void Main()
        {
            Execute().Wait();
        }


        public string Sendemail { get; set; }
        static async Task Execute()
        {

            
            

            var apiKey = Environment.GetEnvironmentVariable("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");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("CodeHub@gmail.com", " User");
            var subject = "Password";
            var to = new EmailAddress(from.Email);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

    }
}