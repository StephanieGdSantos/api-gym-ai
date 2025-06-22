using api_gym_ai.Interfaces.Adapters;
using api_gym_ai.Models;
using System.Text.Json;

namespace api_gym_ai.Adapters
{
    public class JsonAdapter : IJsonAdapter
    {
        public string ExtrairMensagemDoChat(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new Exception("A resposta do serviço está vazia.");

            var jsonResponse = JsonSerializer.Deserialize<JsonElement>(json);

            if (!jsonResponse.TryGetProperty("message", out var message))
                throw new JsonException("Propriedade 'message' não encontrada no JSON.");

            if (!message.TryGetProperty("content", out var content))
                throw new JsonException("Propriedade 'content' não encontrada em 'message'.");

            if (content.ValueKind != JsonValueKind.Array)
                throw new JsonException("A propriedade 'content' não é um array.");

            if (content.GetArrayLength() == 0)
                throw new JsonException("O array 'content' está vazio.");

            if (!content[0].TryGetProperty("text", out var text))
                throw new JsonException("Propriedade 'text' não encontrada no primeiro elemento de 'content'.");

            return text.GetString();
        }
    }
}
