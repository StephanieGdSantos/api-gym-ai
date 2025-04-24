namespace api_gym_ai.Models
{
    public class VariacaoDeTreino
    {
        public string Dia { get; set; } = string.Empty;
        public string MusculosTrabalhados { get; set; } = string.Empty;
        public List<Exercicio> Exercicio { get; set; } = new List<Exercicio>();

        public VariacaoDeTreino(string dia, List<Exercicio> exercicio, string musculosTrabalhados)
        {
            Dia = dia;
            Exercicio = exercicio;
            MusculosTrabalhados = musculosTrabalhados;
        }
    }
}
