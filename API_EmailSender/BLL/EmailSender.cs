using MailKit.Net.Smtp;
using MimeKit;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.IO;



namespace API_EmailSender
{
    public class EmailSender
    {

        [Obsolete("SendEmailWithAPI is deprecated, please use SendEmailWithSmtp instead.")]
        /// <summary>
        /// Uses the Mailgun API to send the email.
        /// </summary>
        /// <param name="emailOfReceiver"></param>
        /// <param name="nameOfReceiver"></param>
        /// <param name="qdto"></param>
        /// <returns></returns>
        public static IRestResponse SendEmailWithAPI(Models.Email email, Models.Quote_DTO qdto, Models.WikiPage_DTO wdto) {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3/mail.doh09.dk");
            client.Authenticator =
            new HttpBasicAuthenticator("api",
                                      "key-a8ed2cbcaa5fc8b0d9ea79b3a4ff6846");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "mail.doh09.dk", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "DailyQuote <postmaster@mail.doh09.dk>");
            request.AddParameter("to", email.NameOfReceiver+ "<"+email.Address+">");
            request.AddParameter("subject", "Hello "+ email.NameOfReceiver + "");
            request.AddParameter("text", "Congratulations "+ email.NameOfReceiver + ", you just received a famous quote!  You are truly awesome!" +
                "\n" +
                "\n <h1> --- Quote and author --- </h1>" +
                "\n '"+ qdto.quote+ "'" +
                "\n - "+ qdto.author
                +"\n "
                );

            request.Method = Method.POST;
            return client.Execute(request);
        }

        /// <summary>
        /// Sends an email via Mailguns SMTP service. Be sure that the 587 port is open for SMTP protocol sending, otherwise it will fail when trying to connect.
        /// </summary>
        /// <param name="emailOfReceiver"></param>
        /// <param name="nameOfReceiver"></param>
        /// <param name="qdto"></param>
        /// <param name="wdto"></param>
        public static void SendEmailWithSmtp(Models.Email email, Models.Quote_DTO qdto, Models.WikiPage_DTO wdto)
        {
            // Compose a message
            MimeMessage mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("Quotes by email", "postmaster@mail.doh09.dk"));
            mail.To.Add(new MailboxAddress(email.NameOfReceiver, email.Address));
            mail.Subject = "Quote by email";
            mail.Body = new TextPart("plain")
            {
                Text = @"Hello " + email.NameOfReceiver +
                ", As requested, here is a famous quote!" +
                "\n" +
                "\n --- Quote and author ---" +
                "\n '" + qdto.quote + "'" +
                "\n - " + qdto.author +
                "\n " +
                "\n " +
                "\n  --- About the author ---"
                +  "\n " + wdto.WikiText
            };

            // Send it!
            using (var client = new SmtpClient())
            {
                // XXX - Should this be a little different?
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.mailgun.org", 2525, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("postmaster@mail.doh09.dk", "aa4d8017e6956903466fb608bbed5edc-116e1e4d-b159f380");

                client.Send(mail);
                client.Disconnect(true);
            }
        }

    }
}
