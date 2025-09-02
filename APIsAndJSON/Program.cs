using System.Net.Http;
using System.Threading.Channels;
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
            }

            Console.WriteLine("\n— End of conversation —");
        }

        private static async Task<string> GetKanyeQuoteAsync()
        {
            var json = await http.GetStringAsync("https://api.kanye.rest/");
            var obj = JObject.Parse(json);
            return (string)obj["quote"];
        }

        private static async Task<string> GetRonQuoteAsync()
        {
            var json = await http.GetStringAsync("https://ron-swanson-quotes.herokuapp.com/v2/quotes");
            var arr = JArray.Parse(json);
            return arr[0].ToString();
        
        }
    }
}
