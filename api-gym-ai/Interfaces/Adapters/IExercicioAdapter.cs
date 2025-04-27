using api_gym_ai.Models;

namespace api_gym_ai.Interfaces.Adapters
{
    public interface IExercicioAdapter
    {
        public List<Exercicio> ListarExerciciosPropostos(string treinoProposto);
    }
}
