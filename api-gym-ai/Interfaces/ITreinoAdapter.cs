using api_gym_ai.Models;

namespace api_gym_ai.Interfaces
{
    public interface ITreinoAdapter
    {
        public Treino? MontarTreino(Pessoa pessoa);
    }
}
