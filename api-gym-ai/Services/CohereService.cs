using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using api_gym_ai.Interfaces.Services;
using api_gym_ai.Options;
using Microsoft.Extensions.Options;

namespace api_gym_ai.Services;
public class CohereService : ICohereService
{
    private readonly HttpClient _httpClient;
    private readonly CohereServiceOptions _cohereServiceOptions;

    public CohereService(HttpClient httpClient, IOptions<CohereServiceOptions> cohereServiceOptions)
    {
        _httpClient = httpClient;
        _cohereServiceOptions = cohereServiceOptions.Value;
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

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _cohereServiceOptions.ApiKey);

        try
        {
            var response = await _httpClient.PostAsync(_cohereServiceOptions.BaseUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"Erro da API Cohere: {error}");
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException($"Erro ao se conectar com a API Cohere: {ex.Message}");
        }
        catch (JsonException ex)
        {
            throw new JsonException($"Erro ao processar a resposta da API Cohere: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Erro inesperado: {ex.Message}");
        }
    }
}