using System.Net;

namespace api_gym_ai.Models
{
    public class Resposta<T>
    {
        public T? Dados { get; set; }
        public string? Mensagem { get; set; }
        public bool Sucesso { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
