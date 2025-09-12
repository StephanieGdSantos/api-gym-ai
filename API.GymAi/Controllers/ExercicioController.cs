using API.GymAi.Adapters.Interfaces;
using API.GymAi.Builders;
using API.GymAi.Facades;
using API.GymAi.Models;
using API.GymAi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace API.GymAi.Controllers
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
        [ProducesResponseType(typeof(Resposta<Treino>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Resposta<Treino>), StatusCodes.Status400BadRequest)]
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
}
