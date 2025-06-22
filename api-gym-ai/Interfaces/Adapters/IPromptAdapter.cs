using api_gym_ai.Models;

namespace api_gym_ai.Interfaces.Adapters
{
    public interface IPromptAdapter
    {
        public Prompt ConstruirPrompt(Pessoa pessoa);
    }
}