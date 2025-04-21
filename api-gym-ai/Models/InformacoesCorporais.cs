namespace api_gym_ai.Models
{
    public class InformacoesCorporais
    {
        public required int Idade { get; set; }
        public required double Altura { get; set; }
        public required double Peso { get; set; }
        public double? PercentualGordura { get; set; }
        public double? MassaMuscular { get; set; }
        public string? Sexo { get; set; }
        public required IEnumerable<string> Limitacoes { get; set; }
        public required string Objetivo { get; set; }
        public required IEnumerable<string> PartesDoCorpoEmFoco { get; set; }
        public required double TempoDeTreino { get; set; }
    }
}
