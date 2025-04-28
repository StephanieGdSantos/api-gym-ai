using api_gym_ai.Builders;
using api_gym_ai.Interfaces.Adapters;
using api_gym_ai.Interfaces.Builders;
using api_gym_ai.Interfaces.Services;
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
            try
            {
                var response = await _cohereService.ChatAsync(prompt.Mensagem);

                if (string.IsNullOrWhiteSpace(response))
                {
                    throw new Exception("A resposta do serviço está vazia.");
                }

                var jsonResponse = JsonSerializer.Deserialize<JsonElement>(response);

                if (!jsonResponse.TryGetProperty("message", out var message) ||
                    !message.TryGetProperty("content", out var content) ||
                    content.ValueKind != JsonValueKind.Array ||
                    content.GetArrayLength() == 0 ||
                    !content[0].TryGetProperty("text", out var text))
                {
                    throw new JsonException("O JSON não contém as propriedades esperadas.");
                }

                return text.GetString();
            }
            catch (JsonException ex)
            {
                throw new JsonException("Erro ao processar o JSON da resposta.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao extrair a resposta do chat.", ex);
            }
        }

        public Prompt ConstruirPrompt(Pessoa pessoa)
        {
            if (pessoa == null)
            {
                throw new ArgumentNullException(nameof(pessoa), "A pessoa não pode ser nula.");
            }

            var promptFinal = _promptBuilder
                .ComIdade(pessoa.Idade.ToString())
                .ComPeso(pessoa.Peso.ToString())
                .ComAltura(pessoa.Altura.ToString())
                .ComMassaMuscular(pessoa.InfoCorporais.MassaMuscular?.ToString() ?? string.Empty)
                .ComPercentualDeGordura(pessoa.InfoCorporais.PercentualGordura?.ToString() ?? string.Empty)
                .ComLimitacoes(string.Join(", ", pessoa.InfoCorporais.Limitacoes ?? Enumerable.Empty<string>()))
                .ComPartesDoCorpoEmFoco(string.Join(", ", pessoa.InfoPreferencias.PartesDoCorpoEmFoco ?? Enumerable.Empty<string>()))
                .ComObjetivo(pessoa.InfoPreferencias.Objetivo)
                .ComTempoDeTreino(pessoa.InfoPreferencias.TempoDeTreino.ToString())
                .ComVariacaoDeTreino(pessoa.InfoPreferencias.VariacaoTreino)
                .ComVariacaoMuscular(pessoa.InfoPreferencias.VariacaoMuscular)
                .Build();

            return promptFinal;
        }
    }
}
