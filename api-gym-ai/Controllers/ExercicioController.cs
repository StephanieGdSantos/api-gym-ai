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
        /*private readonly string _promptBase = "Haja como um personal trainer e, considerando que tenho [idade] anos, [peso]kg, [limitações], desejo trabalhar [partesDoCorpoEmFoco] para [objetivo]; sabendo disso, me diga exercícios para fazer em até [tempo] minutos na academia. me responda utilizando especificamente o formato '[exercício 1], [séries]x[repetições], [músculo(s) alvo separados por espaço]\n [exercício 2], [séries]x[repetições], [músculo(s) alvo separados por espaço]', onde os itens com chaves são mascaras para serem preenchidas; exclua as chaves do texto.não quero mais informações além das pedidas. a mensagem retornada devem seguir exatamente o modelo passado";*/

        public ExercicioController(ICohereService cohereService)
        {
            _cohereService = cohereService;
        }

        [HttpPost]
        public async Task<IEnumerable<Exercicio>> Post([FromBody] Usuario informacoesUsuario)
        {
            var promptFinal = new PromptBuilder()
                .ComIdade(informacoesUsuario.InfoCorporais.Idade)
                .ComPeso(informacoesUsuario.InfoCorporais.Peso)
                .ComAltura(informacoesUsuario.InfoCorporais.Altura)
                .ComMassaMuscular(informacoesUsuario.InfoCorporais.MassaMuscular)
                .ComPercentualDeGordura(informacoesUsuario.InfoCorporais.PercentualGordura)
                .ComLimitacoes(string.Join(", ", informacoesUsuario.InfoCorporais.Limitacoes))
                .ComPartesDoCorpoEmFoco(string.Join(", ", informacoesUsuario.Objetivo.PartesDoCorpoEmFoco))
                .ComObjetivo(informacoesUsuario.Objetivo.Objetivo)
                .ComTempoDeTreino(informacoesUsuario.Objetivo.TempoDeTreino)
                .ComVariacaoDeTreino(informacoesUsuario.Objetivo.VariacaoTreino)
                .Build();
            var retornoChat = await _cohereService.ChatAsync(promptFinal.Mensagem);

            List<Exercicio> listaExercicios = ExercicioFacade.ListarExerciciosPropostos(retornoChat);

            return listaExercicios;
        }
    }
}
