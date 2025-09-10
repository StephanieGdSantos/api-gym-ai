using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using API.GymAi.Options;
using API.GymAi.Repositories.Interface;
using API.GymAi.Services.Interface;
using Microsoft.Extensions.Options;

namespace API.GymAi.Services;
public class ChatService : IChatService
{
    private readonly IChatRepository _chatRepository;

    public ChatService(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<string> ChatAsync(string prompt)
    {
        return await _chatRepository.SendAsync(prompt);
    }
}