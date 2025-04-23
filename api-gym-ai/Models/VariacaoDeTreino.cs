namespace api_gym_ai.Models
{
    public class VariacaoDeTreino
    {
        public string Dia { get; set; } = string.Empty;
        public string MusculosTrabalhados { get; set; } = string.Empty;
        public List<Exercicio> Exercicio { get; set; } = new List<Exercicio>();
    }
}
