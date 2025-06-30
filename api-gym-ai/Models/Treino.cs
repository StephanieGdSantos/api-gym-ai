using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api_gym_ai.Models
{
    public class Treino
    {
        [JsonPropertyName("variacaoDeTreino")]
        public List<VariacaoDeTreino> VariacaoDeTreino { get; set; }

        public PeriodoTreino Periodo { get; set; }
    }

    public class PeriodoTreino
    {
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
    }
}