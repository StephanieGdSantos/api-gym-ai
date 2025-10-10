using System.Text.Json.Serialization;

namespace APIGymAi.Models;

/// <summary>
/// Representa um exerc�cio com informa��es sobre nome, s�ries, repeti��es e m�sculos alvo.
/// </summary>
public class Exercicio
{
    /// <summary>
    /// Nome do exerc�cio.
    /// </summary>
    [JsonPropertyName("nome")]
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// N�mero de s�ries do exerc�cio.
    /// </summary>
    [JsonPropertyName("series")]
    public int Series { get; set; }

    /// <summary>
    /// Descri��o das repeti��es do exerc�cio.
    /// </summary>
    [JsonPropertyName("repeticoes")]
    public string Repeticoes { get; set; } = string.Empty;

    /// <summary>
    /// Lista de m�sculos alvo do exerc�cio.
    /// </summary>
    [JsonPropertyName("musculoAlvo")]
    public IEnumerable<string>? MusculoAlvo { get; set; }
}