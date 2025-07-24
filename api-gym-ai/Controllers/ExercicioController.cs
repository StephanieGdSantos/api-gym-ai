using api_gym_ai.Builders;
using api_gym_ai.Facades;
using api_gym_ai.Interfaces.Adapters;
using api_gym_ai.Models;
using api_gym_ai.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace api_gym_ai.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExercicioController : ControllerBase
    {
        private readonly ITreinoAdapter _treinoAdapter;

        public ExercicioController(ITreinoAdapter treinoAdapter)
        {
            _treinoAdapter = treinoAdapter;
        }

        [HttpPost]
        public async Task<ActionResult<Resposta<Treino>>> Post([FromBody] Pessoa pessoa)
        {
            try
            {
                var resposta = new Resposta<Treino>();
                var treinoProposto = await _treinoAdapter.MontarTreino(pessoa);

                if (treinoProposto == null)
                {
                    return NotFound(new Resposta<Treino>
                    { 
                        Dados = null, 
                        Mensagem = "Erro ao montar o treino", 
                        Sucesso = false, 
                        StatusCode = HttpStatusCode.NotFound
                    });
                }

                return Ok(new Resposta<Treino>
                {
                    Dados = treinoProposto,
                    Mensagem = "Treino montado com sucesso",
                    Sucesso = true,
                    StatusCode = HttpStatusCode.OK
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Resposta<Treino>
                {
                    Dados = null,
                    Mensagem = $"Erro ao processar a solicitação: {ex.Message}",
                    Sucesso = false,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
        }
    }
}
