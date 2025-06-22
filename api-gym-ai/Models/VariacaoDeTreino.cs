using System.Text.Json.Serialization;

namespace api_gym_ai.Models
{
    public class VariacaoDeTreino
    {
        [JsonPropertyName("dia")]
        public string Dia { get; set; } = string.Empty;
        [JsonPropertyName("musculosTrabalhados")]
        public IEnumerable<string> MusculosTrabalhados { get; set; }
        [JsonPropertyName("exercicio")]
        public IEnumerable<Exercicio> Exercicio { get; set; }

        public VariacaoDeTreino(string dia, IEnumerable<Exercicio> exercicio, IEnumerable<string> musculosTrabalhados)
        {
            Dia = dia;
            Exercicio = exercicio;
            MusculosTrabalhados = musculosTrabalhados;
        }
    }
}
