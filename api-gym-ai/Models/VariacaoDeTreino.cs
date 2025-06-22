using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api_gym_ai.Models
{
    public class VariacaoDeTreino
    {
        [JsonPropertyName("dia")]
        public string Dia { get; set; }

        [JsonPropertyName("musculosTrabalhados")]
        public IEnumerable<string> MusculosTrabalhados { get; set; }

        [JsonPropertyName("exercicios")]
        public IEnumerable<Exercicio> Exercicio { get; set; }
    }
}
