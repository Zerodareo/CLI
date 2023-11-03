using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

class Corrector
{

    static async Task Main(string[] args)
    {
        // string word = "Bhonchour je shuis Billy";
        string api_key = "3ca2ac433f91d741a0fca452bd0b944da8adbb13";

        var HttpClient = new HttpClient();

        string url = "https://api.nlpcloud.io/v1/gpu/finetuned-llama-2-70b/gs-correction";
        

        //  string correctedText = await CorrectTextWithOpenAI(api_key, word);
         Console.WriteLine("Que veux-tu corriger ?");
         string word = Console.ReadLine();

        var requestData = new
        {
            text = word
        };
        var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {api_key}");

        var response = await httpClient.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Texte corrigé : ");
            Console.WriteLine(responseContent);
        }
        else
        {
            Console.WriteLine($"Erreur : {response.StatusCode}");
        }
    
        // Console.WriteLine("Texte corrigé : " + correctedText);
    }
}
 