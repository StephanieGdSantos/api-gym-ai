namespace api_gym_ai.Exceptions;

public class JsonChatException : Exception
{
    public JsonChatException(string message) : base(message) { }
    public JsonChatException(string message, Exception innerException) : base(message, innerException) { }
}
