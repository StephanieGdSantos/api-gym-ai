using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using api_gym_ai.Interfaces;

namespace api_gym_ai.Services;
public class CohereService : ICohereService
{
    private readonly HttpClient _httpClient;
    private const string ApiKey = "COHERE_API_KEY";
    private const string Url = "https://api.cohere.com/v2/chat";

    public CohereService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> ChatAsync(string prompt)
    {
        var requestBody = new
        {
            stream = false,
            model = "command-a-03-2025",
            messages = new[]
            {
            new { role = "user", content = prompt }
        }
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);

        var response = await _httpClient.PostAsync(Url, content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"Erro da API Cohere: {error}");
        }

        return await response.Content.ReadAsStringAsync();
    }
}