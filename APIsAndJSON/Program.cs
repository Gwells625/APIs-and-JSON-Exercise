using System;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
namespace APIsAndJSON
{
    public class Program
    {
        private static readonly HttpClient http = new HttpClient();

        static async Task Main(string[] args)
        {
            Console.WriteLine("Ron Swanson  vs Kanye West \n");

            for (int i = 0; i < 5; i++)
            {
                string kanye = await GetKanyeQuoteAsync();
                Console.WriteLine($"Kanye: \"{kanye}\"");

                string ron = await GetRonQuoteAsync();
                Console.WriteLine($"Ron: \"{ron}\"");

                Console.WriteLine();
            }

            Console.WriteLine("— End of conversation —\n");

            const string keyfile = "appsettings.json";
            if (!File.Exists(keyfile))
            {
                Console.WriteLine("appsettings.json not found. Create it with: { \"APIKey\": \"YOUR_KEY\" }");
                return;
            }


            string keyJson = File.ReadAllText(keyfile);
            string APIKey = JObject.Parse(keyJson).GetValue("APIKey").ToString();
            if (string.IsNullOrEmpty(APIKey))
            {
                Console.WriteLine("APIKey missing from appsettings.json");
                return;
            }

            Console.WriteLine("Please enter your zipcode:");
            var zipCode = Console.ReadLine();

            string apiCall =
                $"https://api.openweathermap.org/data/2.5/weather?zip={zipCode}&units=imperial&appid={APIKey}";

            Console.WriteLine();

            double temp = OpenWeatherMapAPI.GetTemp(apiCall);
            Console.WriteLine($"\n It is currently: {temp:F1} degrees in your location.");
            
        }

        private static async Task<string> GetKanyeQuoteAsync() 
        {
            var json = await http.GetStringAsync("https://api.kanye.rest/");
            var obj = JObject.Parse(json);
            return (string)obj["quote"];
        }

        static async Task<string> GetRonQuoteAsync()
        {
            var json = await http.GetStringAsync("https://ron-swanson-quotes.herokuapp.com/v2/quotes");
            var arr = JArray.Parse(json);
            return arr[0].ToString();
        }

    }
}
   

        
    

