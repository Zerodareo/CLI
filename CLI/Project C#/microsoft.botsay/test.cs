using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

class Program
{
    static async Task Main(string[] args)
    {
        string apiKey = "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";
        string textToCorrect = "YVOICI une Bhanane"; // Texte à corriger

        string correctedText = await CorrectTextWithOpenAI(apiKey, textToCorrect);
        
        Console.WriteLine("Texte original : " + textToCorrect);
        Console.WriteLine("Texte corrigé : " + correctedText);
    }

    static async Task<string> CorrectTextWithOpenAI(string apiKey, string textToCorrect)
    {
        using (HttpClient client = new HttpClient())
        {
            string apiUrl = "https://api.openai.com/v1/engines/text-davinci-002/completions"; // Endpoint de l'API GPT-3
            string prompt = $"Corrige le texte suivant : '{textToCorrect}'"; // Utilisez le texte comme prompt

            var requestData = new
            {
                prompt = prompt,
                max_tokens = 50 // Nombre maximal de tokens dans la réponse
            };

            var content = new StringContent(JsonSerializer.Serialize(requestData));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            HttpResponseMessage response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var responseJson = JsonSerializer.Deserialize<OpenAIResponse>(responseBody);

                if (responseJson.choices.Count > 0)
                {
                    return responseJson.choices[0].text;
                }
            }

            return "La correction a échoué.";
        }
    }
}

public class OpenAIResponse
{
    public List<Choice> choices { get; set; }
}

public class Choice
{
    public string text { get; set; }

    }
    

