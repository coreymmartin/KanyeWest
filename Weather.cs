using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.IO;

namespace KanyeWest
{
    public class Weather
    {
        private HttpClient _client;
        private string apiKey = JObject.Parse(File.ReadAllText("appsettings.json")).GetValue("WeatherMachine").ToString();

        public Weather(HttpClient client)
        {
            _client = client;
        }

        public int AskForZip()
        {
            bool gotZip = false;
            bool zipGood = false;
            int zipCode;
            do
            {
                Console.WriteLine("please enter zip code to get the current weather\nenter 0 to exit...\n");
                gotZip = int.TryParse(Console.ReadLine(), out zipCode);
                zipGood = (gotZip && 0 <= zipCode && zipCode <= 99999);
            } while (!zipGood);
            return zipCode;
        }

        public void GetWeatherByZip()
        {
            int userZip = AskForZip();
            if (userZip > 0)
            {
                try
                {
                    var weatherURL = $"https://api.openweathermap.org/data/2.5/weather?zip={userZip},us&appid={apiKey}&units=imperial";
                    var weatherResponse = _client.GetStringAsync(weatherURL).Result;
                    var weatherCity = JObject.Parse(weatherResponse).GetValue("name").ToString();
                    var weatherData = JObject.Parse(weatherResponse).GetValue("main").ToString();
                    var weatherTemp = JObject.Parse(weatherData).GetValue("temp").ToString();
                    var weatherFeelsLike = JObject.Parse(weatherData).GetValue("feels_like").ToString();
                    var weatherInfo = JObject.Parse(weatherResponse).GetValue("weather")[0].ToString();
                    var weatherDesc = JObject.Parse(weatherInfo).GetValue("description").ToString();
                    var weatherWind = JObject.Parse(weatherResponse).GetValue("wind").ToString();
                    var weatherWindSpeed = JObject.Parse(weatherWind).GetValue("speed").ToString();
                    Console.Clear();
                    Console.WriteLine($"\nthe temperature for {weatherCity} is {weatherTemp}");
                    Console.WriteLine($"but really it feels like {weatherFeelsLike}");
                    Console.WriteLine($"the wind speed is {weatherWindSpeed} mph and the skies are looking like {weatherDesc}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"whoa there buddy! there was a problem with your request. sorry about that");
                    Console.WriteLine($"error: {e}");
                    Thread.Sleep(3000);
                }
            }
        }
    }
}
