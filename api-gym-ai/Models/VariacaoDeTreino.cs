namespace api_gym_ai.Models
{
    public class VariacaoDeTreino
    {
        public string Dia { get; set; } = string.Empty;
        public IEnumerable<string> MusculosTrabalhados { get; set; }
        public IEnumerable<Exercicio> Exercicio { get; set; }

        public VariacaoDeTreino(string dia, IEnumerable<Exercicio> exercicio, IEnumerable<string> musculosTrabalhados)
        {
            Dia = dia;
            Exercicio = exercicio;
            MusculosTrabalhados = musculosTrabalhados;
        }
    }
}
