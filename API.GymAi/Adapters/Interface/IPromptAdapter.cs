using API.GymAi.Models;

namespace API.GymAi.Adapters.Interfaces
{
    public interface IPromptAdapter
    {
        public Prompt ConstruirPrompt(Pessoa pessoa);
    }
}