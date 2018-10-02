using System;
using System.Threading.Tasks;

namespace QuoteEmailer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Quote();
            Email();
            //EmailValidation();
            //Wiki();
        }

        static void Wiki() {
            Console.WriteLine("Hello and welcome to the wikipage fetcher!");
            Console.WriteLine("wikipage fetched:");
            WikipediaFetcher wf = new WikipediaFetcher();
            Console.WriteLine(wf.GetWikipediaPage("Henry Ford"));
            Console.WriteLine("Click >ENTER< to exit...");
            Console.ReadLine();
        }

        static void Quote() {
            Console.WriteLine("Hello and welcome to the quote fetcher!");
            Console.WriteLine("Quote fetched:");
            QuoteFetcher qf = new QuoteFetcher();
            Quote q = qf.GetQuote();
            Console.WriteLine(q.quote);
            WikipediaFetcher wf = new WikipediaFetcher();
            Console.WriteLine(wf.GetWikipediaPage(q.author));
            Console.WriteLine("Click >ENTER< to exit...");
            Console.ReadLine();
        }

        static void Email() {
            Console.WriteLine("Hello and welcome to the quote emailer!");
            Console.WriteLine("What is your email?");
            string email = Console.ReadLine();
            Console.WriteLine("What is your name?");
            string name = Console.ReadLine();
            QuoteFetcher qf = new QuoteFetcher();
            Quote q = qf.GetQuote();
            WikipediaFetcher wf = new WikipediaFetcher();
            WikiPage wikiPage = wf.GetWikipediaPage(q.author);
            Console.WriteLine("Adding quote: '" + q.quote+ "' to email.");
            EmailSender.SendEmailWithSmtp(email, name, q, wikiPage);
            Console.WriteLine("Email sent to " + email);
            Console.WriteLine("Click >ENTER< to exit...");
            Console.ReadLine();
        }

        //https://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address
        /*
         For most use cases, a false "invalid" is much worse for your users and future proofing than a false "valid".
             */
        static void EmailValidation()
        {
            string input = "";
            while (!input.ToLower().Equals("quit"))
            {
                Console.WriteLine("Write an email or write 'quit'");
                input = Console.ReadLine();
                if (input.ToLower().Equals("quit"))
                {
                    break;
                }
                Console.WriteLine("Email appeared valid: " + EmailValidator.IsValidEmail(input));
            }
        }
    }
}
