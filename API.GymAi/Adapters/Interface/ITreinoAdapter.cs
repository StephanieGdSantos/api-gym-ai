using API.GymAi.Models;

namespace API.GymAi.Adapters.Interfaces
{
    public interface ITreinoAdapter
    {
        public Task<Treino?> MontarTreino(Pessoa pessoa);
    }
}
