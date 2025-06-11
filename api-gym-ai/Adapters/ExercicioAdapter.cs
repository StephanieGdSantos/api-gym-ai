using api_gym_ai.Builders;
using api_gym_ai.Interfaces.Adapters;
using api_gym_ai.Interfaces.Builders;
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
            var exercicios = treinoProposto
                .Split(new[] { "_" }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            var listaExercicios = new List<Exercicio>();

            exercicios.ForEach(exercicio =>
            {
                var partes = exercicio
                    .Split([','], StringSplitOptions.RemoveEmptyEntries);

                if (partes.Length == 3)
                {
                    var nomeExercicio = partes[0]
                        .Trim();

                    var numeroRepeticoes = partes[1]
                        .Split("x");

                    var series = int.Parse(numeroRepeticoes[0]
                        .Trim());

                    var repeticoes = numeroRepeticoes[1]
                        .Trim();

                    var musculosAlvo = partes[2]
                        .Trim()
                        .Split(" ");

                    var novoExercicio = ConstruirExercicio(nomeExercicio, series, repeticoes, musculosAlvo);

                    listaExercicios.Add(novoExercicio);
                }
            });

            return listaExercicios;
        }

        public Exercicio ConstruirExercicio(string nome, int series, string repeticoes, IEnumerable<string> musculosAlvo)
        {
            return _exercicioBuilder
                .ComNome(nome)
                .ComSeries(series)
                .ComRepeticoes(repeticoes)
                .ComMusculosAlvo(musculosAlvo)
                .Build();
        }
    }
}
