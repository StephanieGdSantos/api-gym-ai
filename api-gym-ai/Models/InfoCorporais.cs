using System.ComponentModel.DataAnnotations;

namespace api_gym_ai.Models
{
    public class InfoCorporais
    {
        [Required(ErrorMessage = "A idade é obrigatória.")]
        [Range(10, 100, ErrorMessage = "A idade deve ser um valor válido, entre 10 e 100.")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "A altura é obrigatória")]
        [Range(1.30, 2.80, ErrorMessage = "A altura deve ser um valor válido, entre 1,30m e 2,80m.")]
        public double Altura { get; set; }

        [Required(ErrorMessage = "O peso é obrigatório.")]
        [Range(1, 500, ErrorMessage = "O peso deve ser maior que 0 e menor que 500.")]
        public double Peso { get; set; }

        [Range(10, 50, ErrorMessage = "O percentual de gordura deve ser maior que 10 e menor que 50.")]
        public double? PercentualGordura { get; set; }

        [Range(15, 50, ErrorMessage = "A massa muscular deve ser maior que 15 e menor que 50.")]
        public double? MassaMuscular { get; set; }
        public string? Sexo { get; set; }
        public IEnumerable<string>? Limitacoes { get; set; }
    }
}
