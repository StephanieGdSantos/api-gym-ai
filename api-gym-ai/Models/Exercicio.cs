namespace api_gym_ai.Models
{
    public class Exercicio
    {
        public required string Nome { get; set; } = string.Empty;
        public required int Series { get; set; }
        public required string Repeticoes { get; set; }
        public required IEnumerable<string>? MusculoAlvo { get; set; }
    }
}
