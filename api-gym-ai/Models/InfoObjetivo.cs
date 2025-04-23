using System.ComponentModel.DataAnnotations;

namespace api_gym_ai.Models
{
    public class InfoObjetivo
    {
        [Required(ErrorMessage = "O objetivo do treino é obrigatório.")]
        public string Objetivo { get; set; } = string.Empty;

        [Required(ErrorMessage = "É obrigatório informar as partes do corpo que deseja focar.")]
        public IEnumerable<string> PartesDoCorpoEmFoco { get; set; } = new List<string>();

        [Required(ErrorMessage = "O tempo de treino é obrigatório.")]
        [Range(20, 240, ErrorMessage = "O tempo de treino deve ser de 20 minutos a 4 horas.")]
        public double TempoDeTreino { get; set; }

        [Required(ErrorMessage = "A variação de treino é obrigatória.")]
        public string VariacaoTreino { get; set; } = string.Empty;
    }
}
