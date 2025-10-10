using System.Net;

namespace APIGymAi.Models;
/// <summary>
/// Representa uma resposta genérica com dados, status de sucesso e código de status HTTP.
/// </summary>
/// <typeparam name="T">O tipo dos dados retornados na resposta.</typeparam>
public class Resposta<T>
{
    /// <summary>
    /// Obtém ou define os dados retornados na resposta.
    /// </summary>
    public T? Dados { get; set; }

    /// <summary>
    /// Obtém ou define um valor que indica se a operação foi bem-sucedida.
    /// </summary>
    public bool Sucesso { get; set; }

    /// <summary>
    /// Obtém ou define o código de status HTTP associado à resposta.
    /// </summary>
    public int StatusCode { get; set; }

    public Resposta() { }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Resposta{T}"/>.
    /// </summary>
    /// <param name="dados">Os dados retornados na resposta.</param>
    /// <param name="sucesso">Indica se a operação foi bem-sucedida.</param>
    /// <param name="statusCode">O código de status HTTP associado à resposta.</param>
    public Resposta(T? dados, bool sucesso, int statusCode)
    {
        Dados = dados;
        Sucesso = sucesso;
        StatusCode = statusCode;
    }
}
