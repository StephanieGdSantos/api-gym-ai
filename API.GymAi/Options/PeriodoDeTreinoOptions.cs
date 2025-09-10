using System.ComponentModel.DataAnnotations;

namespace API.GymAi.Options
{
    public class PeriodoDeTreinoOptions
    {
        [Required]
        public int Iniciante { get; set; }
        [Required]
        public int Intermediario { get; set; }
        [Required]
        public int Avancado { get; set; }
    }
}
