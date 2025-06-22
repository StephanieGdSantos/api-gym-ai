using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api_gym_ai.Models
{
    public class Treino
    {
        [JsonPropertyName("variacaoDeTreino")]
        public List<VariacaoDeTreino> VariacaoDeTreino { get; set; }

        [JsonPropertyName("dataInicio")]
        public string DataInicio { get; set; }

        [JsonPropertyName("dataFim")]
        public string DataFim { get; set; }
    }
}