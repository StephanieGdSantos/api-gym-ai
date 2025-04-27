using api_gym_ai.Models;

namespace api_gym_ai.Interfaces
{
    public interface IVariacaoDeTreinoAdapter
    {
        public List<VariacaoDeTreino> ListarVariacaoDeTreinos(Task<string> retornoChat);
    }
}
