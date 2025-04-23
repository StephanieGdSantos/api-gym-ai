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

        public ExercicioController(ICohereService cohereService)
        {
            _cohereService = cohereService;
        }

        [HttpPost]
        public async Task<IEnumerable<Exercicio>> Post([FromBody] Usuario informacoesUsuario)
        {
            if (informacoesUsuario?.InfoCorporais == null || informacoesUsuario.Objetivo == null)
            {
                throw new ArgumentNullException(nameof(informacoesUsuario), "As informações do usuário não podem ser nulas.");
            }

            var promptFinal = new PromptBuilder()
                .ComIdade(informacoesUsuario.InfoCorporais.Idade.ToString())
                .ComPeso(informacoesUsuario.InfoCorporais.Peso.ToString())
                .ComAltura(informacoesUsuario.InfoCorporais.Altura.ToString())
                .ComMassaMuscular(informacoesUsuario.InfoCorporais.MassaMuscular?.ToString() ?? string.Empty)
                .ComPercentualDeGordura(informacoesUsuario.InfoCorporais.PercentualGordura?.ToString() ?? string.Empty)
                .ComLimitacoes(string.Join(", ", informacoesUsuario.InfoCorporais.Limitacoes ?? Enumerable.Empty<string>()))
                .ComPartesDoCorpoEmFoco(string.Join(", ", informacoesUsuario.Objetivo.PartesDoCorpoEmFoco ?? Enumerable.Empty<string>()))
                .ComObjetivo(informacoesUsuario.Objetivo.Objetivo)
                .ComTempoDeTreino(informacoesUsuario.Objetivo.TempoDeTreino.ToString())
                .ComVariacaoDeTreino(informacoesUsuario.Objetivo.VariacaoTreino)
                .Build();

            var retornoChat = await _cohereService.ChatAsync(promptFinal.Mensagem);

            List<Exercicio> listaExercicios = ExercicioFacade.ListarExerciciosPropostos(retornoChat);

            return listaExercicios;
        }
    }
}
