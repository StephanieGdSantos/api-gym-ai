using api_gym_ai.Builders;
using api_gym_ai.Interfaces.Adapters;
using api_gym_ai.Interfaces.Builders;
using api_gym_ai.Models;

namespace api_gym_ai.Facades
{
    public class VariacaoDeTreinoAdapter: IVariacaoDeTreinoAdapter
    {
        private readonly IExercicioAdapter _exercicioAdapter;

        public VariacaoDeTreinoAdapter(IExercicioAdapter exercicioAdapter)
        {
            _exercicioAdapter = exercicioAdapter;
        }

        public List<VariacaoDeTreino> ListarVariacaoDeTreinos(string retornoChat)
        {
            try
            {
                var variacaoDeTreinosSplit = retornoChat
                    .Split('|')
                    .ToList();

                var listaVariacaoDeTreinos = new List<VariacaoDeTreino>();

                variacaoDeTreinosSplit.ForEach(treino =>
                {
                    var treinoSplit = treino
                        .Split('-')
                        .ToList();

                    var exerciciosDoTreino = _exercicioAdapter.ListarExerciciosPropostos(treinoSplit[1]);

                    var treinoProposto = new VariacaoDeTreino(
                        treinoSplit[0],
                        exerciciosDoTreino,
                        treinoSplit[2]
                    );

                    listaVariacaoDeTreinos.Add(treinoProposto);
                });

                return listaVariacaoDeTreinos;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar variações de treino: " + ex.Message);
            }
        }
    }
}
