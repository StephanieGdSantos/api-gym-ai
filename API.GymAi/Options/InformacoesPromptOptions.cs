using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace API.GymAi.Options
{
    /// <summary>  
    /// Representa as opções de configuração para informações do prompt.  
    /// </summary>  
    public class InformacoesPromptOptions
    {
        /// <summary>  
        /// Obtém ou define a quantidade mínima de exercícios.  
        /// </summary>  
        [Required]
        public int QuantidadeMinimaExercicios { get; set; }

        /// <summary>  
        /// Obtém ou define a quantidade máxima de exercícios.  
        /// </summary>  
        [Required]
        public int QuantidadeMaximaExercicios { get; set; }

        /// <summary>  
        /// Obtém ou define o texto base do prompt.  
        /// </summary>  
        [Required]
        public string BasePrompt { get; set; }
    }
}
