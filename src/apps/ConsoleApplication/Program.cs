using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using SAM;

namespace ConsoleApplication
{
    // the following example uses JSON.NET from Newtonsoft to read JSON responses and output to console
    class Program
    {
        private static SamClient _sam;
        private static ResponseFormat _format = ResponseFormat.JSON; // default

        static void Main(string[] args)
        {
            // To get an API key go to "API Access" settings page inside SAM.
            // This is availabe to account admins.

            _sam = new SamClient(AuthType.API_KEY, "<API_KEY>");

            PromptForQuery();
        }

        private static void PromptForQuery()
        {
            Console.WriteLine("For documentation of each end point see http://api.samdesk.io/");
            Console.WriteLine("1 - get account");
            Console.WriteLine("2 - get list of stories");
            Console.WriteLine("<story id> - get account (ie 52c7791ded6b77800d000007)");
            Console.WriteLine(string.Format("3 - switch output format (JSON or XML). Current format is {0}.", _format.ToString()));

            Console.WriteLine("Press enter to exit");
            string input = Console.ReadLine();

            if (input == "")
            {
                Console.WriteLine("Thanks for using SAM.");
            }
            else
            {
                var val = Int32.Parse(input);

                if (val == 1)
                {
                    GetAccount();
                }
                else if (val == 2)
                {
                    GetStories();
                }
                else if (val == 3)
                {
                    ChangeOutputFormat();
                }
                else
                {
                    GetStory(input);
                }
            }
        }

        private static void ChangeOutputFormat()
        {
            Console.WriteLine("Press 1 for JSON or 2 for XML");
            var formatString = Console.ReadLine();

            var formatInt = Convert.ToInt32(formatString);
            if (formatInt == 1) _format = ResponseFormat.JSON;
            else if (formatInt == 2) _format = ResponseFormat.XML;
            else
            {
                Console.WriteLine("Invalid format.");
            }

            PromptForQuery();
        }

        private static void GetAccount()
        {
            var account = _sam.RetrieveAccount();
            Output(account);
            PromptForQuery();
        }

        private static void GetStories()
        {
            var stories = _sam.ListStories();
            Output(stories);
            PromptForQuery();
        }

        private static void GetStory(string storyId)
        {
            var story = _sam.RetrieveStory(storyId);
            Output(story);
            PromptForQuery();
        }

        private static void Output(object obj)
        {
            string output;

            if (_format == ResponseFormat.JSON)
            {
                output = JsonConvert.SerializeObject(obj, Formatting.Indented);
            }
            else
            {
                output = Utils.ToXml(obj);
            }

            // optionally could dump to file here...
            Console.WriteLine(output);
        }
    }
}
