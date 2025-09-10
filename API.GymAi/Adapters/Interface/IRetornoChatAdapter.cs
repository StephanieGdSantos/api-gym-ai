using API.GymAi.Models;

namespace API.GymAi.Adapters.Interfaces
{
    public interface IRetornoChatAdapter
    {
        public Task<string> ExtrairRespostaDoChat(Prompt prompt);
        public string FormatarRetornoDoChat(string mensagemChat);
    }
}
