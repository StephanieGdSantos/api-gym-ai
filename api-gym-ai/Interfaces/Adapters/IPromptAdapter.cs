using api_gym_ai.Models;

namespace api_gym_ai.Interfaces.Adapters
{
    public interface IPromptAdapter
    {
        public Task<string> ExtrairRespostaDoChat(Prompt prompt);
        public Prompt ConstruirPrompt(Pessoa pessoa);
    }
}