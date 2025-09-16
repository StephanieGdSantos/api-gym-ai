using System.Runtime.Serialization;

namespace API.GymAi.Exceptions;
/// <summary>
/// Representa uma exceção que ocorre durante a desserialização de JSON.
/// </summary>
public class JsonDeserializationException(string message, Exception innerException) : Exception(message, innerException), ISerializable
{
}
