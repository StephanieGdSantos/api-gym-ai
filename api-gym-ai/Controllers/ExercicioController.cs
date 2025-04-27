using api_gym_ai.Builders;
using api_gym_ai.Facades;
using api_gym_ai.Interfaces;
using api_gym_ai.Models;
using api_gym_ai.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace api_gym_ai.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExercicioController
    {
        private readonly ITreinoAdapter _treinoAdapter;

        public ExercicioController(ITreinoAdapter treinoAdapter)
        {
            _treinoAdapter = treinoAdapter;
        }

        [HttpPost]
        [ValidarPessoa]
        public async Task<Treino> Post([FromBody] Pessoa pessoa)
        {
            var treinoProposto = _treinoAdapter.MontarTreino(pessoa); 

            return treinoProposto;
        }
    }
}
