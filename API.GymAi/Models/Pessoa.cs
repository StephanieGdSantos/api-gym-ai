using System.ComponentModel.DataAnnotations;

namespace API.GymAi.Models
{
    /// <summary>
    /// Representa uma pessoa com informações corporais e preferências de treino.
    /// </summary>
    public class Pessoa
    {
        public const int IdadeMinima = 10;

        /// <summary>
        /// Idade máxima permitida para a pessoa.
        /// </summary>
        public const int IdadeMaxima = 100;

        /// <summary>
        /// Altura mínima permitida para a pessoa em metros.
        /// </summary>
        public const double AlturaMinima = 1.30;

        /// <summary>
        /// Altura máxima permitida para a pessoa em metros.
        /// </summary>
        public const double AlturaMaxima = 2.80;

        /// <summary>
        /// Peso mínimo permitido para a pessoa em quilogramas.
        /// </summary>
        public const double PesoMinimo = 1;

        /// <summary>
        /// Peso máximo permitido para a pessoa em quilogramas.
        /// </summary>
        public const double PesoMaximo = 500;

        /// <summary>
        /// Idade da pessoa. Deve estar entre 10 e 100 anos.
        /// </summary>
        [Required(ErrorMessage = "A idade é obrigatória.")]
        [Range(IdadeMinima, IdadeMaxima, ErrorMessage = "A idade deve ser um valor válido, entre 10 e 100.")]
        public int Idade { get; set; }

        /// <summary>
        /// Altura da pessoa em metros. Deve estar entre 1,30m e 2,80m.
        /// </summary>
        [Required(ErrorMessage = "A altura é obrigatória")]
        [Range(AlturaMinima, AlturaMaxima, ErrorMessage = "A altura deve ser um valor válido, entre 1,30m e 2,80m.")]
        public double Altura { get; set; }

        /// <summary>
        /// Peso da pessoa em quilogramas. Deve estar entre 1kg e 500kg.
        /// </summary>
        [Required(ErrorMessage = "O peso é obrigatório.")]
        [Range(PesoMinimo, PesoMaximo, ErrorMessage = "O peso deve ser maior que 0 e menor que 500.")]
        public double Peso { get; set; }

        /// <summary>
        /// Informações corporais adicionais da pessoa.
        /// </summary>
        [Required(ErrorMessage = "O fornecimento de informações corporais é obrigatório.")]
        public InfoCorporais? InfoCorporais { get; set; }

        /// <summary>
        /// Preferências de treino da pessoa.
        /// </summary>
        [Required(ErrorMessage = "É obrigatório especificar as preferências de treino.")]
        public InfoPreferencias? InfoPreferencias { get; set; }
    }
}
