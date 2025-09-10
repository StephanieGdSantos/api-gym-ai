using System.Text.Json;
using API.GymAi.Exceptions;

namespace API.GymAi.Utils
{
    public static class JsonUtils
    {
        public static T DeserializeOrThrow<T>(string json, string? context = null)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(json)
                    ?? throw new JsonDeserializationException(
                        $"Falha ao desserializar o JSON{(context != null ? $" para {context}" : "")}: objeto nulo.",
                        null!);
            }
            catch (JsonException ex)
            {
                throw new JsonDeserializationException(
                    $"Erro ao desserializar o JSON{(context != null ? $" para {context}" : "")}.", ex);
            }
        }
    }
}
