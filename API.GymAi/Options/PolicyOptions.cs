using System.ComponentModel.DataAnnotations;

namespace APIGymAi.Options;

/// <summary>
/// Representa as opções de configuração para as medidas de resiliência.
/// </summary>
public class PolicyOptions
{
    /// <summary>
    /// Número máximo de exceções permitidas antes de o circuito "cair".
    /// </summary>
    [Required]
    public int NumeroMaximoDeExcecoesAntesDeCair { get; set; }

    /// <summary>
    /// Intervalo que o circuito permanecerá "caído" antes de tentar se recuperar.
    /// </summary>
    [Required]
    public int IntervaloDeQuedaEmSegundos { get; set; }

    /// <summary>
    /// Número máximo de retentativas para operações falhas.
    /// </summary>
    [Required]
    public int QuantidadeMaximaDeRetentativas { get; set; }

    /// <summary>
    /// Intervalo em segundos para aguardar antes de tentar novamente uma operação falha.
    /// </summary>
    [Required]
    public int IntervaloEmSegundosParaAguardarAntesDeTentarNovamente { get; set; }
}
