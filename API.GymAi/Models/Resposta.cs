using System.Net;

namespace API.GymAi.Models;
public class Resposta<T>
{
    public T? Dados { get; set; }
    public bool Sucesso { get; set; }
    public int StatusCode { get; set; }

    public Resposta(T? dados, bool sucesso, int statusCode)
    {
        Dados = dados;
        Sucesso = sucesso;
        StatusCode = statusCode;
    }
}