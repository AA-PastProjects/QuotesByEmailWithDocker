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

        /// <summary>
        /// Sends an email via Mailguns SMTP service. Be sure that the port used is open for SMTP protocol sending, otherwise it will fail when trying to connect.
        /// </summary>
        /// <param name="emailOfReceiver"></param>
        /// <param name="nameOfReceiver"></param>
        /// <param name="qdto"></param>
        /// <param name="wdto"></param>
        public static void SendEmailWithSmtp(Models.Email email, Models.Quote_DTO qdto, Models.WikiPage_DTO wdto)
        {
            // Compose a message
            MimeMessage mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("Quotes by email", "INSERT EMAIL TO SEND FROM"));
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

                client.Connect("smtp.mailgun.org", 2525, false); //can use port 587 too. Depends on which is open. 2525 looks to be more modernly used though.
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("INSERT EMAIL TO SEND FROM", "INSERT API PASSWORD");

                client.Send(mail);
                client.Disconnect(true);
            }
        }

    }
}
