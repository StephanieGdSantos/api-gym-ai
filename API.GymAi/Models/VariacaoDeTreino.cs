using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.GymAi.Models
{
    /// <summary>  
    /// Representa uma variação de treino, incluindo o dia, os músculos trabalhados e os exercícios.  
    /// </summary>  
    public class VariacaoDeTreino
    {
        /// <summary>  
        /// O dia da semana associado à variação de treino.  
        /// </summary>  
        [JsonPropertyName("dia")]
        public string Dia { get; set; }

        /// <summary>  
        /// Lista de músculos trabalhados nesta variação de treino.  
        /// </summary>  
        [JsonPropertyName("musculosTrabalhados")]
        public IEnumerable<string> MusculosTrabalhados { get; set; }

        /// <summary>  
        /// Lista de exercícios incluídos nesta variação de treino.  
        /// </summary>  
        [JsonPropertyName("exercicios")]
        public IEnumerable<Exercicio> Exercicio { get; set; }
    }
}
