using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIGymAi.Models;

/// <summary>
/// Representa um treino, contendo variações de treino e o período do treino.
/// </summary>
public class Treino
{
    /// <summary>
    /// Lista de variações de treino associadas a este treino.
    /// </summary>
    [JsonPropertyName("variacaoDeTreino")]
    public List<VariacaoDeTreino> VariacaoDeTreino { get; set; }

    /// <summary>
    /// Período do treino, incluindo data de início e fim.
    /// </summary>
    public PeriodoTreino Periodo { get; set; }
}

/// <summary>
/// Representa o período de um treino, com data de início e fim.
/// </summary>
public class PeriodoTreino
{
    /// <summary>
    /// Data de início do período do treino.
    /// </summary>
    public string DataInicio { get; set; }

    /// <summary>
    /// Data de fim do período do treino.
    /// </summary>
    public string DataFim { get; set; }
}