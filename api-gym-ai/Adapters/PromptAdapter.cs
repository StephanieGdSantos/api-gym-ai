using api_gym_ai.Interfaces.Adapters;
using api_gym_ai.Interfaces.Builders;
using api_gym_ai.Models;

namespace api_gym_ai.Facades
{
    public class PromptAdapter : IPromptAdapter
    {
        private readonly IPromptBuilder _promptBuilder;

        public PromptAdapter(IPromptBuilder promptBuilder)
        {
            _promptBuilder = promptBuilder;
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
