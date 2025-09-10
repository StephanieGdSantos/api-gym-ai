using API.GymAi.Options;
using API.GymAi.Repositories.Interface;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Options;

namespace API.GymAi.Repositories
{
    public class CohereRepository : IChatRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ChatOptions _chatRepositoryOptions;

        public CohereRepository(HttpClient httpClient, IOptions<ChatOptions> cohereServiceOptions)
        {
            _httpClient = httpClient;
            _chatRepositoryOptions = cohereServiceOptions.Value;
        }

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
}
