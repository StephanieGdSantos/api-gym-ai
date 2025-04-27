using api_gym_ai.Builders;
using api_gym_ai.Interfaces;
using api_gym_ai.Models;

namespace api_gym_ai.Facades
{
    public class VariacaoDeTreinoAdapter: IVariacaoDeTreinoAdapter
    {
        private readonly IExercicioAdapter _exercicioAdapter;
        private readonly IExercicioBuilder _exercicioBuilder;

        public VariacaoDeTreinoAdapter(IExercicioAdapter exercicioAdapter, IExercicioBuilder exercicioBuilder)
        {
            _exercicioAdapter = exercicioAdapter;
            _exercicioBuilder = exercicioBuilder;
        }

        public List<VariacaoDeTreino> ListarVariacaoDeTreinos(Task<string> retornoChat)
        {
            var variacaoDeTreinosSplit = retornoChat
                .Result
                .Split('|')
                .ToList();

            var listaVariacaoDeTreinos = new List<VariacaoDeTreino>();
            foreach (var treino in variacaoDeTreinosSplit)
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
            }

            return listaVariacaoDeTreinos;
        }
    }
}
