using System.ComponentModel.DataAnnotations;

namespace API.GymAi.Models
{
    public class Pessoa
    {
        public const int IdadeMinima = 10;
        public const int IdadeMaxima = 100;
        public const double AlturaMinima = 1.30;
        public const double AlturaMaxima = 2.80;
        public const double PesoMinimo = 1;
        public const double PesoMaximo = 500;

        [Required(ErrorMessage = "A idade é obrigatória.")]
        [Range(IdadeMinima, IdadeMaxima, ErrorMessage = "A idade deve ser um valor válido, entre 10 e 100.")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "A altura é obrigatória")]
        [Range(AlturaMinima, AlturaMaxima, ErrorMessage = "A altura deve ser um valor válido, entre 1,30m e 2,80m.")]
        public double Altura { get; set; }

        [Required(ErrorMessage = "O peso é obrigatório.")]
        [Range(PesoMinimo, PesoMaximo, ErrorMessage = "O peso deve ser maior que 0 e menor que 500.")]
        public double Peso { get; set; }

        [Required(ErrorMessage = "O fornecimento de informações corporais é obrigatório.")]
        public InfoCorporais? InfoCorporais { get; set; }

        [Required(ErrorMessage = "É obrigatório especificar as preferências de treino.")]
        public InfoPreferencias? InfoPreferencias { get; set; }
    }
}
