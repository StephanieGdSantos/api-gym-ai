using api_gym_ai.Models;

namespace api_gym_ai.Interfaces.Adapters
{
    public interface ITreinoAdapter
    {
        public Task<Treino?> MontarTreino(Pessoa pessoa);
    }
}
