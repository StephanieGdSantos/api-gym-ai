namespace api_gym_ai.Models
{
    public class InfoCorporais
    {
        public string Idade { get; set; }
        public string Altura { get; set; }
        public string Peso { get; set; }
        public string? PercentualGordura { get; set; }
        public string? MassaMuscular { get; set; }
        public string? Sexo { get; set; }
        public IEnumerable<string> Limitacoes { get; set; }
    }
}
