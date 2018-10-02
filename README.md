# QuotesByEmailWithDocker

Project from the spring of 2018 - made during my 1st semester of my PBA in software development.

It is a web api application made in .NET Core with C#. It is also made with Docker, Docker-compose and Swagger.

There is some error handling that could have been done more nicely and a static method which some would argue should have been a non-static method.

But overall a good project that works as intended.

### Setup

To set up the application you need to do 3 steps.

1. Follow Mailguns instructions on their website for setting up a user on their website + DNS values on your domain.
2. Wait for the DNS to be discovered correctly (can take some time, maybe minutes, maybe hours, maybe a day or two).
3. In the EmailSender.cs class, edit the SendEmailWithSmtp method to fit your new mailgun acquired information.
4. Ensure you have correctly set up docker/docker-compose and that the docker-compose project is set as the startup project.

The reason this setup is required is that the sending of the email itself in this program goes via MailGuns service and is hosted as a docker-compose file.

### Features and use

If this application is set up correctly it will do the following.

1. Show a console menu prompting for an email and a name.
2. A superficial check to see if the email appears valid (e.g. has a @ symbol in it).
3. Contacts an external API which in JSON replies with a quote and the author of the quote.
4. Separate the author out from the JSON reply.
5. Contacts wikipedias API with the authors name and gets a JSON reply.
6. Fetches the first section of the wikipedia page JSON.
7. Creates an email with both the quote, author and wikipedia information.
8. Sends the email to the given email address via an SMTP protocol that goes to the external mail service Mailgun.

If everything went as intended (meaning your email was correct + your docker and mailgun settings were set up correctly) you will shortly after get an email with the content acquired by the service.