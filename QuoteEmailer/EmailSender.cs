using MailKit.Net.Smtp;
using MimeKit;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.IO;



namespace QuoteEmailer
{
    public class EmailSender
    {

        public static IRestResponse SendEmailWithAPI(string emailOfReceiver, string nameOfReceiver, Quote q) {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3/mail.doh09.dk");
            client.Authenticator =
            new HttpBasicAuthenticator("api",
                                      "key-a8ed2cbcaa5fc8b0d9ea79b3a4ff6846");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "mail.doh09.dk", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "test <postmaster@mail.doh09.dk>");
            request.AddParameter("to", "test <martingustavsen1994@gmail.com>");
            request.AddParameter("subject", "Hello "+ nameOfReceiver + "");
            request.AddParameter("text", "Congratulations "+ nameOfReceiver + ", you just received a famous quote!  You are truly awesome!" +
                "\n" +
                "\n <h1> --- Quote and author --- </h1>" +
                "\n '"+ q.quote+ "'" +
                "\n - "+q.author
                +"\n "
                );

            request.Method = Method.POST;
            return client.Execute(request);
        }

        public static void SendEmailWithSmtp(string emailOfReceiver, string nameOfReceiver, Quote q, WikiPage wikiPage)
        {
            // Compose a message
            MimeMessage mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("Quotes by email", "postmaster@mail.doh09.dk"));
            mail.To.Add(new MailboxAddress(nameOfReceiver, emailOfReceiver));
            mail.Subject = "Quote by email";
            mail.Body = new TextPart("plain")
            {
                Text = @"Hello " + nameOfReceiver +
                ", As requested, here is a famous quote!" +
                "\n" +
                "\n --- Quote and author ---" +
                "\n '" + q.quote + "'" +
                "\n - " + q.author +
                "\n " +
                "\n " +
                "\n  --- About the  author ---"
                +  "\n " + wikiPage.wikiText
            };

            // Send it!
            using (var client = new SmtpClient())
            {
                // XXX - Should this be a little different?
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.mailgun.org", 25, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("postmaster@mail.doh09.dk", "aa4d8017e6956903466fb608bbed5edc-116e1e4d-b159f380");

                client.Send(mail);
                client.Disconnect(true);
            }
        }

    }
}
