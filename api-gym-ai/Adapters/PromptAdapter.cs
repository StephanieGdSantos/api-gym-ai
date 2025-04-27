using api_gym_ai.Builders;
using api_gym_ai.Interfaces;
using api_gym_ai.Models;
using api_gym_ai.Services;
using System.Text.Json;

namespace api_gym_ai.Facades
{
    public class PromptAdapter : IPromptAdapter
    {
        private readonly ICohereService _cohereService;
        private readonly IPromptBuilder _promptBuilder;

        public PromptAdapter(ICohereService cohereService, IPromptBuilder promptBuilder)
        {
            _cohereService = cohereService;
            _promptBuilder = promptBuilder;
        }

        public async Task<string> ExtrairRespostaDoChat(Prompt prompt)
        {
            var retornoChat = await _cohereService.ChatAsync(prompt.Mensagem);
            if (string.IsNullOrEmpty(retornoChat))
                throw new Exception("Erro ao obter resposta do chat.");

            var conteudoJson = JsonDocument.Parse(retornoChat);
            var root = conteudoJson.RootElement;
            var conteudoGeral = root.GetProperty("message").GetProperty("content");
            var respostaChat = conteudoGeral[0].GetProperty("text").GetString();

            if (respostaChat == null)
            {
                throw new Exception("Erro ao extrair a resposta do chat.");
            }
            return respostaChat;
        }

        public Prompt ConstruirPrompt(Pessoa pessoa)
        {
            var promptFinal = _promptBuilder
                .ComIdade(pessoa.Idade.ToString())
                .ComPeso(pessoa.Peso.ToString())
                .ComAltura(pessoa.Altura.ToString())
                .ComMassaMuscular(pessoa.InfoCorporais.MassaMuscular?.ToString() ?? string.Empty)
                .ComPercentualDeGordura(pessoa.InfoCorporais.PercentualGordura?.ToString() ?? string.Empty)
                .ComLimitacoes(string.Join(", ", pessoa.InfoCorporais.Limitacoes ?? Enumerable.Empty<string>()))
                .ComPartesDoCorpoEmFoco(string.Join(", ", pessoa.Objetivo.PartesDoCorpoEmFoco ?? Enumerable.Empty<string>()))
                .ComObjetivo(pessoa.Objetivo.Objetivo)
                .ComTempoDeTreino(pessoa.Objetivo.TempoDeTreino.ToString())
                .ComVariacaoDeTreino(pessoa.Objetivo.VariacaoTreino)
                .Build();

            if (promptFinal == null)
                throw new Exception("Erro ao construir o prompt.");

            return promptFinal;
        }
    }
}
