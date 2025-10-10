using System.ComponentModel.DataAnnotations;

namespace APIGymAi.Models;

/// <summary>
/// Representa informações corporais de um indivíduo, incluindo percentual de gordura, massa muscular, sexo e limitações.
/// </summary>
public class InfoCorporais
{
    /// <summary>
    /// Percentual de gordura do indivíduo. Deve estar entre 10 e 50.
    /// </summary>
    [Range(10, 50, ErrorMessage = "O percentual de gordura deve ser maior que 10 e menor que 50.")]
    public double? PercentualGordura { get; set; }

    /// <summary>
    /// Massa muscular do indivíduo. Deve estar entre 15 e 50.
    /// </summary>
    [Range(15, 50, ErrorMessage = "A massa muscular deve ser maior que 15 e menor que 50.")]
    public double? MassaMuscular { get; set; }

    /// <summary>
    /// Sexo do indivíduo.
    /// </summary>
    public string? Sexo { get; set; }

    /// <summary>
    /// Lista de limitações físicas ou de saúde do indivíduo.
    /// </summary>
    public IEnumerable<string>? Limitacoes { get; set; }
}
