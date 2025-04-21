using api_gym_ai.Builders;

namespace api_gym_ai.Models
{
    public class Exercicio
    {
        public string Nome { get; set; } = string.Empty;
        public int Series { get; set; }
        public string Repeticoes { get; set; } = string.Empty;
        public IEnumerable<string>? MusculoAlvo { get; set; }
    }
}