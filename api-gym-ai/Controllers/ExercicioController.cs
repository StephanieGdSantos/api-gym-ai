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
        private readonly ICohereService _cohereService;
        private readonly ITreinoAdapter _treinoAdapter;
        private readonly IPromptAdapter _promptAdapter;

        public ExercicioController(ICohereService cohereService, ITreinoAdapter treinoAdapter, IPromptAdapter promptAdapter)
        {
            _cohereService = cohereService;
            _treinoAdapter = treinoAdapter;
            _promptAdapter = promptAdapter;
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
