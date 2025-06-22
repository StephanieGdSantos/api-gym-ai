using api_gym_ai.Builders;
using System.Text.Json.Serialization;

namespace api_gym_ai.Models
{
    public class Exercicio
    {
        [JsonPropertyName("nome")]
        public string Nome { get; set; } = string.Empty;
        [JsonPropertyName("series")]
        public int Series { get; set; }
        [JsonPropertyName("repeticoes")]
        public string Repeticoes { get; set; } = string.Empty;
        [JsonPropertyName("musculoAlvo")]
        public IEnumerable<string>? MusculoAlvo { get; set; }

        public Exercicio()
        {
        }

        public Exercicio(string nome, int series, string repeticoes, IEnumerable<string>? musculoAlvo)
        {
            Nome = nome;
            Series = series;
            Repeticoes = repeticoes;
            MusculoAlvo = musculoAlvo;
        }
    }
}