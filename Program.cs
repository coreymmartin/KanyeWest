using System;
using System.Net.Http;
using System.Threading;

namespace KanyeWest
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            var wiseman = new Wiseman(client);
            var weather = new Weather(client);
            wiseman.WiseManOnceSaid();
            Thread.Sleep(3000);
            do
            {
                Console.WriteLine("press enter to hear more quotes." +
                    "\nenter \"weather\" to get weather for a US city" +
                    "\nenter \"exit\" to quit application");
                string userInput = Console.ReadLine();
                if (userInput.ToLower() == "weather")
                {
                    weather.GetWeatherByZip();
                    Thread.Sleep(1000);
                    Console.WriteLine("\npress enter to continue...\n");
                    Console.ReadLine();
                }                   
                else if (userInput.ToLower() == "exit")
                    break;
                else
                    wiseman.WiseManOnceSaid();
            } while (true);
        }
    }
}
