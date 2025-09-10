using System.Text.Json.Serialization;

namespace API.GymAi.Models
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
    }
}