using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using APIGymAi.Options;
using APIGymAi.Repositories.Interface;
using APIGymAi.Services.Interface;
using Microsoft.Extensions.Options;

namespace APIGymAi.Services;
/// <summary>  
/// Service responsible for handling chat-related operations.  
/// </summary>  
public class ChatService : IChatService
{
    private readonly IChatRepository _chatRepository;

    /// <summary>  
    /// Initializes a new instance of the <see cref="ChatService"/> class.  
    /// </summary>  
    /// <param name="chatRepository">The repository used to send chat prompts.</param>  
    public ChatService(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    /// <summary>  
    /// Sends a chat prompt and retrieves the response.  
    /// </summary>  
    /// <param name="prompt">The chat prompt to send.</param>  
    /// <returns>A task that represents the asynchronous operation. The task result contains the chat response.</returns>  
    public async Task<string> ChatAsync(string prompt)
    {
        return await _chatRepository.SendAsync(prompt);
    }
}
