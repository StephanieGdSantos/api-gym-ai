using APIGymAi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace APIGymAi.RespostaSwaggerExample;

public class TreinoBadRequestExample : IExamplesProvider<Resposta<Treino>>
{
    public Resposta<Treino> GetExamples()
    {
        return new Resposta<Treino>(
            dados: null,
            sucesso: false,
            statusCode: 400
        );
    }
}