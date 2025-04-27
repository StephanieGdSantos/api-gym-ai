using api_gym_ai.Builders;
using api_gym_ai.Interfaces;
using api_gym_ai.Models;
using System.Text.Json;

namespace api_gym_ai.Facades
{
    public class ExercicioAdapter : IExercicioAdapter
    {
        private readonly IExercicioBuilder _exercicioBuilder;
        public ExercicioAdapter(IExercicioBuilder exercicioBuilder)
        {
            _exercicioBuilder = exercicioBuilder;
        }

        public List<Exercicio> ListarExerciciosPropostos(string treinoProposto)
        {
            var exercicios = treinoProposto.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var listaExercicios = new List<Exercicio>();
            foreach (var item in exercicios)
            {
                var partes = item.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (partes.Length == 3)
                {
                    var nomeExercicio = partes[0].Trim();
                    var numeroRepeticoes = partes[1].Split("x");
                    var series = int.Parse(numeroRepeticoes[0].Trim());
                    var repeticoes = numeroRepeticoes[1].Trim();
                    var musculosAlvo = partes[2].Trim().Split(" ");

                    var novoExercicio = _exercicioBuilder
                        .ComNome(nomeExercicio)
                        .ComSeries(series)
                        .ComRepeticoes(repeticoes)
                        .ComMusculosAlvo(musculosAlvo)
                        .Build();

                    listaExercicios.Add(novoExercicio);
                }
            }

            return listaExercicios;
        }
    }
}
