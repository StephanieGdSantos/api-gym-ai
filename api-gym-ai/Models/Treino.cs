using System.ComponentModel.DataAnnotations;

namespace api_gym_ai.Models
{
    public class Treino
    {
        public List<VariacaoDeTreino> VariacaoDeTreino { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}