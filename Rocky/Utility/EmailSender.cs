using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(email, subject, htmlMessage);
        }

        public async Task Execute(string email, string subject, string body)
        {
            MailjetClient client = new MailjetClient("edc3c39eda17f26abe0491375d78ae78","ab7cea1252d1e96d3335df1198d9bffd")
            {
                Version = ApiVersion.V3_1,
            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
             .Property(Send.Messages, new JArray {
     new JObject {
      {
       "From",
       new JObject {
        {"Email", "deniskutchenko008@gmail.com"},
        {"Name", "Denis"}
       }
      }, {
       "To",
       new JArray {
        new JObject {
         {
          "Email",
          email
         }, {
          "Name",
          "Web Administrator"
         }
        }
       }
      }, {
       "Subject",
       subject
      },{
       "HTMLPart",
       body

     }
             }});
            await client.PostAsync(request);
        }
    }
}
