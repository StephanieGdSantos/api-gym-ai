using APIGymAi.Adapters.Interface;
using APIGymAi.Builders;
using APIGymAi.Models;
using APIGymAi.RespostaSwaggerExample;
using APIGymAi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using System.Text.Json;

namespace APIGymAi.Controllers;

/// <summary>  
/// Controlador responsável por gerenciar os exercícios e propor treinos com base nos dados da pessoa.  
/// </summary>  
[ApiController]
[Route("[controller]")]
public class ExercicioController : ControllerBase
{
    private readonly ITreinoAdapter _treinoAdapter;

    /// <summary>  
    /// Inicializa uma nova instância de <see cref="ExercicioController"/>.  
    /// </summary>  
    /// <param name="treinoAdapter">Dependência para montagem de treinos.</param>  
    public ExercicioController(ITreinoAdapter treinoAdapter)
    {
        _treinoAdapter = treinoAdapter;
    }

    /// <summary>  
    /// Endpoint para propor um treino com base nos dados da pessoa fornecida.  
    /// </summary>  
    /// <param name="pessoa">Objeto contendo os dados da pessoa.</param>  
    /// <returns>Resposta contendo o treino proposto ou um erro.</returns>  
    [HttpPost]
    [ProducesResponseType(typeof(Resposta<Treino>), StatusCodes.Status200OK)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(TreinoOkExample))]
    [ProducesResponseType(typeof(Resposta<Treino>), StatusCodes.Status400BadRequest)]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(TreinoBadRequestExample))]
    public async Task<ActionResult> Post([FromBody] Pessoa pessoa)
    {
        var treinoProposto = await _treinoAdapter.MontarTreino(pessoa);

        if (treinoProposto == null)
        {
            return BadRequest(new Resposta<Treino>(
                dados: null,
                sucesso: false,
                statusCode: StatusCodes.Status400BadRequest
            ));
        }

        return Ok(new Resposta<Treino>(
            dados: treinoProposto,
            sucesso: true,
            statusCode: StatusCodes.Status200OK
        ));
    }
}
