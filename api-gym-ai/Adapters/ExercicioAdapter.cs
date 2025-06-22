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

        public List<Exercicio> ListarExerciciosPropostos(JsonElement jsonExercicios)
        {
            var listaExercicios = new List<Exercicio>();

            foreach (var exercicio in jsonExercicios.EnumerateArray())
            {
                var novoExercicio = ExtrairExercicio(exercicio);

                listaExercicios.Add(novoExercicio);
            }
            return listaExercicios;
        }

        public Exercicio ExtrairExercicio(JsonElement json)
        {
            var exercicioTemNome = json.TryGetProperty("nome", out var nome);
            var exercicioTemSeries = json.TryGetProperty("series", out var series);
            var exercicioTemRepeticoes = json.TryGetProperty("repeticoes", out var repeticoes);
            var exercicioTemMusculosAlvo = json.TryGetProperty("musculoAlvo", out var musculosAlvo);

            if (!exercicioTemNome || !exercicioTemSeries || !exercicioTemRepeticoes || !exercicioTemMusculosAlvo)
                throw new Exception("Dados de exercício proposto ou descrição ausentes.");

            var listaMusculosAlvo = musculosAlvo
                .EnumerateArray()
                .Select(m => m.GetString() ?? string.Empty)
                .Where(m => !string.IsNullOrWhiteSpace(m))
                .ToList();

            var exercicio = _exercicioBuilder
                .ComNome(nome.GetString() ?? string.Empty)
                .ComSeries(series.GetInt32())
                .ComRepeticoes(repeticoes.GetString() ?? string.Empty)
                .ComMusculosAlvo(listaMusculosAlvo)
                .Build();

            return exercicio;
        }
    }
}
