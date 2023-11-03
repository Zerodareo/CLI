using System;
using openai;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


openai.api_key = ('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')
class Corrector {
    static string ChaineCorrect{
        string ToCorrect = word ;
        string api_key = "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";
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var requestBody = new
            {
                prompt = textToCorrect,
                max_tokens = 50, // Nombre maximal de tokens dans la réponse
            };

            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://api.openai.com/v1/engines/text-davinci-002/completions", content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);
            }
            else
            {
                Console.WriteLine($"Erreur : {response.StatusCode}");
            }
        }
    }
}


class Program
{
    static async Task Main(string[] args)
    {
        string apiKey = "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";
        string tentative = "BHaguette Fromage z'aime le chocolat";

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var requestBody = new
            {
                tentative = tentative,
                max_tokens = 100, // Nombre maximal de tokens dans la réponse
            };

            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://api.openai.com/v1/engines/davinci/completions", content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                // Extrait la réponse de l'API
                var apiResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse>(responseContent);

                // Affiche la réponse générée par OpenAI
                Console.WriteLine(apiResponse.choices[0].text);
            }
            else
            {
                Console.WriteLine($"Erreur : {response.StatusCode}");
            }
        }
    }
}

public class ApiResponse
{
    public string id { get; set; }
    public long object_created { get; set; }
    public string model { get; set; }
    public List<Choice> choices { get; set; }
}

public class Choice
{
    public float finish_reason { get; set; }
    public string index { get; set; }
    public string logprobs { get; set; }
    public float text { get; set; }
    public string token_logprobs { get; set; }
}

