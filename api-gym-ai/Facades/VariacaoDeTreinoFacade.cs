using api_gym_ai.Builders;
using api_gym_ai.Models;

namespace api_gym_ai.Facades
{
    public static class VariacaoDeTreinoFacade
    {
        public static List<VariacaoDeTreino> ListarVariacaoDeTreinos(string retornoChat)
        {
            var variacaoDeTreinosSplit = retornoChat
                .Split('|')
                .ToList();

            var listaVariacaoDeTreinos = new List<VariacaoDeTreino>();
            foreach (var treino in variacaoDeTreinosSplit)
            {
                var treinoSplit = treino
                    .Split('-')
                    .ToList();

                var exerciciosDoTreino = ExercicioFacade.ListarExerciciosPropostos(treinoSplit[1]);

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
