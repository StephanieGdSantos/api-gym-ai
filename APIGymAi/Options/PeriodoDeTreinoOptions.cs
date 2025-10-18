using System.ComponentModel.DataAnnotations;

namespace APIGymAi.Options;

/// <summary>
/// Representa as opções de configuração para os períodos de treino.
/// </summary>
public class PeriodoDeTreinoOptions
{
    /// <summary>
    /// Duração do período de treino para iniciantes (em dias).
    /// </summary>
    [Required]
    public int Iniciante { get; set; }

    /// <summary>
    /// Duração do período de treino para intermediários (em dias).
    /// </summary>
    [Required]
    public int Intermediario { get; set; }

    /// <summary>
    /// Duração do período de treino para avançados (em dias).
    /// </summary>
    [Required]
    public int Avancado { get; set; }
}
