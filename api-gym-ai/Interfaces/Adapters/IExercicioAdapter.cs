using api_gym_ai.Models;
using System.Text.Json;

namespace api_gym_ai.Interfaces.Adapters
{
    public interface IExercicioAdapter
    {
        public List<Exercicio> ListarExerciciosPropostos(JsonElement treinoProposto);
        public Exercicio ExtrairExercicio(JsonElement json);
    }
}
