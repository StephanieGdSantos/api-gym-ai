using api_gym_ai.Builders;
using api_gym_ai.Interfaces.Adapters;
using api_gym_ai.Interfaces.Builders;
using api_gym_ai.Models;
using System.Text.Json;

namespace api_gym_ai.Facades
{
    public class VariacaoDeTreinoAdapter : IVariacaoDeTreinoAdapter
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
                var jsonResponse = JsonSerializer.Deserialize<JsonElement>(retornoChat);

                if (!jsonResponse.TryGetProperty("variacaoDeTreino", out var variacaoDeTreinos))
                    return new List<VariacaoDeTreino>(); //adiciona tratamento de erros depois

                var listaJsonVariacaoDeTreinos = variacaoDeTreinos
                    .EnumerateArray()
                    .ToList();

                var listaVariacaoDeTreinos = new List<VariacaoDeTreino>();

                listaJsonVariacaoDeTreinos.ForEach(jsonVariacao =>
                {
                    var treinoProposto = ExtrairVariacaoDeTreino(jsonVariacao);

                    listaVariacaoDeTreinos.Add(treinoProposto);
                });

                return listaVariacaoDeTreinos;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar variações de treino: " + ex.Message);
            }
        }

        private VariacaoDeTreino ExtrairVariacaoDeTreino(JsonElement json)
        {
            var variacaoTemNome = json.TryGetProperty("dia", out var nomeTreino);
            var variacaoTemMusculos = json.TryGetProperty("musculosTrabalhados", out var musculosTrabalhados);
            var variacaoTemExercicios = json.TryGetProperty("exercicio", out var exercicios);

            if (!variacaoTemNome || !variacaoTemMusculos || !variacaoTemExercicios)
                throw new Exception("Dados de treino proposto ou descrição ausentes.");

            var exerciciosPropostos = _exercicioAdapter.ListarExerciciosPropostos(exercicios);

            var listaMusculosTrabalhados = musculosTrabalhados
            .EnumerateArray()
                .Select(m => m.GetString() ?? string.Empty)
                .Where(m => !string.IsNullOrWhiteSpace(m))
                .ToList();

            var treinoProposto = new VariacaoDeTreino(
                nomeTreino.GetString() ?? string.Empty,
                exerciciosPropostos,
                listaMusculosTrabalhados
            );

            return treinoProposto;
        }
    }
}
