using APIGymAi.Options;
using APIGymAi.Repositories.Interface;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Options;

namespace APIGymAi.Repositories;

/// <summary>  
/// Repositório responsável por interagir com o serviço Cohere para envio de prompts e obtenção de respostas.  
/// </summary>  
public class CohereRepository : IChatRepository
{
    private readonly HttpClient _httpClient;
    private readonly ChatOptions _chatRepositoryOptions;

    /// <summary>  
    /// Inicializa uma nova instância da classe <see cref="CohereRepository"/>.  
    /// </summary>  
    /// <param name="httpClient">Instância de <see cref="HttpClient"/> para realizar requisições HTTP.</param>  
    /// <param name="cohereServiceOptions">Opções de configuração para o serviço Cohere.</param>  
    public CohereRepository(HttpClient httpClient, IOptions<ChatOptions> cohereServiceOptions)
    {
        _httpClient = httpClient;
        _chatRepositoryOptions = cohereServiceOptions.Value;
    }

    /// <summary>  
    /// Envia um prompt para o serviço Cohere e retorna a resposta.  
    /// </summary>  
    /// <param name="prompt">Texto do prompt a ser enviado.</param>  
    /// <returns>Resposta do serviço Cohere como uma string.</returns>  
    public async Task<string> SendAsync(string prompt)
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

        try
        {
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _chatRepositoryOptions.ApiKey);

            var response = await _httpClient.PostAsync(_chatRepositoryOptions.BaseUrl, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro inesperado: {ex.Message}");
        }
    }
}
