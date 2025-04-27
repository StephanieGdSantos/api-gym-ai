using System.ComponentModel.DataAnnotations;

namespace api_gym_ai.Models
{
    public class InfoCorporais
    {
        [Range(10, 50, ErrorMessage = "O percentual de gordura deve ser maior que 10 e menor que 50.")]
        public double? PercentualGordura { get; set; }


        [Range(15, 50, ErrorMessage = "A massa muscular deve ser maior que 15 e menor que 50.")]
        public double? MassaMuscular { get; set; }

        public string? Sexo { get; set; }

        public IEnumerable<string>? Limitacoes { get; set; }
    }
}
