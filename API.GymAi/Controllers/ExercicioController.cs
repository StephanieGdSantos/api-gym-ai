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
        [ProducesResponseType<Treino>(StatusCodes.Status200OK)]
        [ProducesResponseType<Treino>(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Treino>> Post([FromBody] Pessoa pessoa)
        {
            var resposta = new Resposta<Treino>();
            var treinoProposto = await _treinoAdapter.MontarTreino(pessoa);

            return treinoProposto;
        }
    }
}
