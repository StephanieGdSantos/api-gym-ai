using System.Text.Json.Serialization;

namespace APIGymAi.Models;

/// <summary>
/// Representa um exercício com informações sobre nome, séries, repetições e músculos alvo.
/// </summary>
public class Exercicio
{
    /// <summary>
    /// Nome do exercício.
    /// </summary>
    [JsonPropertyName("nome")]
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Número de séries do exercício.
    /// </summary>
    [JsonPropertyName("series")]
    public int Series { get; set; }

    /// <summary>
    /// Descrição das repetições do exercício.
    /// </summary>
    [JsonPropertyName("repeticoes")]
    public string Repeticoes { get; set; } = string.Empty;

    /// <summary>
    /// Lista de músculos alvo do exercício.
    /// </summary>
    [JsonPropertyName("musculoAlvo")]
    public IEnumerable<string>? MusculoAlvo { get; set; }
}