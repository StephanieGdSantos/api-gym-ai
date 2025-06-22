using api_gym_ai.Models;

namespace api_gym_ai.Interfaces.Adapters
{
    public interface IRetornoChatAdapter
    {
        public Task<string> ExtrairRespostaDoChat(Prompt prompt);
        public string FormatarRetornoDoChat(string mensagemChat);
    }
}
