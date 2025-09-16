using API.GymAi.Adapters.Interfaces;
using API.GymAi.Builders.Interfaces;
using API.GymAi.Models;

namespace API.GymAi.Facades
{
    /// <summary>  
    /// Adapter responsável por construir prompts com base nas informações de uma pessoa.  
    /// </summary>  
    public class PromptAdapter(IPromptBuilder promptBuilder) : IPromptAdapter
    {
        private readonly IPromptBuilder _promptBuilder = promptBuilder;

        /// <summary>  
        /// Constrói um prompt com base nas informações fornecidas de uma pessoa.  
        /// </summary>  
        /// <param name="pessoa">A pessoa cujas informações serão usadas para construir o prompt.</param>  
        /// <returns>O prompt construído.</returns>  
        public Prompt ConstruirPrompt(Pessoa pessoa)
        {
            var partesDoCorpoEmFoco = string.Join(", ", pessoa.InfoPreferencias.PartesDoCorpoEmFoco.Select(parteDoCorpo => parteDoCorpo.ToString()));

            var promptFinal = _promptBuilder
                .ComIdade(pessoa.Idade.ToString())
                .ComPeso(pessoa.Peso.ToString())
                .ComAltura(pessoa.Altura.ToString())
                .ComMassaMuscular(pessoa.InfoCorporais?.MassaMuscular?.ToString() ?? string.Empty)
                .ComPercentualDeGordura(pessoa.InfoCorporais?.PercentualGordura?.ToString() ?? string.Empty)
                .ComLimitacoes(string.Join(", ", pessoa.InfoCorporais?.Limitacoes ?? Enumerable.Empty<string>()))
                .ComPartesDoCorpoEmFoco(partesDoCorpoEmFoco)
                .ComObjetivo(pessoa.InfoPreferencias.Objetivo.ToString())
                .ComTempoDeTreinoEmMinutos(pessoa.InfoPreferencias.TempoDeTreinoEmMinutos.ToString())
                .ComVariacaoDeTreino(pessoa.InfoPreferencias.VariacaoTreino)
                .ComObservacao(pessoa.InfoPreferencias.Observacao)
                .ComNivel(pessoa.InfoPreferencias.Nivel.ToString())
                .Build();

            return promptFinal;
        }
    }
}
