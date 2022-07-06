using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace KanyeWest
{
    public class Wiseman
    {
        private HttpClient _client;

        public Wiseman(HttpClient client)
        {
            _client = client;
        }

        public string GetKanyeQuote()
        {
            var kanyeURL = "https://api.kanye.rest/";
            var kanyeResponse = _client.GetStringAsync(kanyeURL).Result;
            var kanyeQuote = JObject.Parse(kanyeResponse).GetValue("quote").ToString();
            return kanyeQuote;
        }

        public string GetRonQuote()
        {
            var ronSwansonURL = "https://ron-swanson-quotes.herokuapp.com/v2/quotes";
            var ronSwansonResponse = _client.GetStringAsync(ronSwansonURL).Result;
            var ronSwansonQuote = JArray.Parse(ronSwansonResponse)[0].ToString();
            return ronSwansonQuote;
        }

        public void WiseManOnceSaid()
        {
            Console.Clear();
            Console.WriteLine($"\na wise man once said: \n\"{GetRonQuote()}\"\nhowever, he also said, \n\"{GetKanyeQuote()}\"\n");
        }

    }
}
