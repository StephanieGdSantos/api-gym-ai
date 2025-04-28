using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using api_gym_ai.Interfaces.Services;

namespace api_gym_ai.Services;
public class CohereService : ICohereService
{
    private readonly HttpClient _httpClient;
    private string _apiKey;
    private const string _url = "https://api.cohere.com/v2/chat";

    public CohereService(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _apiKey = Environment.GetEnvironmentVariable("COHERE_API_KEY") ?? throw new ApplicationException("Chave da API não encontrada.");
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

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

        try
        {
            var response = await _httpClient.PostAsync(_url, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"Erro da API Cohere: {error}");
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseBody);
            if (!jsonResponse.TryGetProperty("message", out var message) ||
                !message.TryGetProperty("content", out var contentProperty))
            {
                throw new ApplicationException("Resposta inválida da API Cohere: JSON não contém as propriedades esperadas.");
            }

            return responseBody;
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Erro ao se conectar com a API Cohere: {ex.Message}");
        }
        catch (JsonException ex)
        {
            throw new ApplicationException($"Erro ao processar a resposta da API Cohere: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Erro inesperado: {ex.Message}");
        }
    }
}