using System.Text.Json;
using APIGymAi.Exceptions;

namespace APIGymAi.Utils;

/// <summary>  
/// Provides utility methods for working with JSON data.  
/// </summary>  
public static class JsonUtils
{
    /// <summary>  
    /// Deserializes a JSON string into an object of type <typeparamref name="T"/>.  
    /// Throws a <see cref="JsonDeserializationException"/> if deserialization fails.  
    /// </summary>  
    /// <typeparam name="T">The type of the object to deserialize to.</typeparam>  
    /// <param name="json">The JSON string to deserialize.</param>  
    /// <param name="context">An optional context description to include in the exception message.</param>  
    /// <returns>The deserialized object of type <typeparamref name="T"/>.</returns>  
    /// <exception cref="JsonDeserializationException">Thrown when deserialization fails.</exception>  
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
