using api_gym_ai.Models;

namespace api_gym_ai.Interfaces.Adapters
{
    public interface IVariacaoDeTreinoAdapter
    {
        public List<VariacaoDeTreino> ListarVariacaoDeTreinos(string retornoChat);
    }
}
