using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace API.GymAi.Options
{
    public class InformacoesPromptOptions
    {
        [Required]
        public int QuantidadeMinimaExercicios { get; set; }
        [Required]
        public int QuantidadeMaximaExercicios { get; set; }
        [Required]
        public string BasePrompt {  get; set; }
    }
}
