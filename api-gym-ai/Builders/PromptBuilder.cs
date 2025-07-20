using api_gym_ai.Interfaces.Builders;
using api_gym_ai.Models;
using api_gym_ai.Options;
using Microsoft.Extensions.Options;
using System.Numerics;
using static api_gym_ai.Models.InfoPreferencias;

namespace api_gym_ai.Builders
{
    public class PromptBuilder : IPromptBuilder
    {
        private readonly IOptions<InformacoesPromptOptions> _informacoesPromptOptions;

        private int _quantidadeMinimaDeExercicios;
        private int _quantidadeMaximaDeExercicios;
        private string _basePrompt;

        private string _informacoes = string.Empty;

        public PromptBuilder(IOptions<InformacoesPromptOptions> informacoesPromptOptions)
        {
            _informacoesPromptOptions = informacoesPromptOptions ?? throw new ArgumentNullException(nameof(informacoesPromptOptions), "As informações do prompt não podem ser nulas.");

            _basePrompt = _informacoesPromptOptions.Value.BasePrompt;
            _quantidadeMaximaDeExercicios = _informacoesPromptOptions.Value.QuantidadeMaximaExercicios;
            _quantidadeMinimaDeExercicios = _informacoesPromptOptions.Value.QuantidadeMinimaExercicios;
        }

        public IPromptBuilder ComAltura(string altura)
        {
            _informacoes += $"Altura: {altura}, ";

            return this;
        }

        public IPromptBuilder ComIdade(string idade)
        {
            _informacoes += $"Idade: {idade}, ";

            return this;
        }

        public IPromptBuilder ComLimitacoes(string? limitacoes)
        {
            if (!string.IsNullOrEmpty(limitacoes))
                _informacoes += $"Limitações: {limitacoes}, ";

            return this;
        }

        public IPromptBuilder ComMassaMuscular(string? massaMuscular)
        {
            if (!string.IsNullOrEmpty(massaMuscular))
                _informacoes += $"Massa Muscular: {massaMuscular}, ";

            return this;
        }

        public IPromptBuilder ComObjetivo(string objetivo)
        {
            _informacoes += $"Objetivo: {objetivo}, ";
            return this;
        }

        public IPromptBuilder ComPartesDoCorpoEmFoco(string partesDoCorpoEmFoco)
        {
            _informacoes += $"Partes do Corpo em Foco: {partesDoCorpoEmFoco}, ";

            return this;
        }

        public IPromptBuilder ComPercentualDeGordura(string? percentualGordura)
        {
            if (!string.IsNullOrEmpty(percentualGordura))
                _informacoes += $"Percentual de Gordura: {percentualGordura}, ";

            return this;
        }

        public IPromptBuilder ComPeso(string peso)
        {
            _informacoes += $"Peso: {peso}, ";

            return this;
        }

        public IPromptBuilder ComSexo(string? sexo)
        {
            if (!string.IsNullOrEmpty(sexo))
                _informacoes += $"Sexo: {sexo}, ";

            return this;
        }

        public IPromptBuilder ComTempoDeTreinoEmMinutos(string tempoDeTreino)
        {
            _informacoes += $"Tempo de Treino: {tempoDeTreino} minutos para cada variação, ";

            return this;
        }

        public IPromptBuilder ComVariacaoDeTreino(string variacao)
        {
            _informacoes += $"Variação de Treino: {variacao}, ";

            return this;
        }

        public IPromptBuilder ComObservacao(string variacaoMuscular)
        {
            _informacoes += $"Variação Muscular: {variacaoMuscular}, ";

            return this;
        }

        public IPromptBuilder ComNivel(string nivel)
        {
            _informacoes += $"Nível: {nivel}, ";
            return this;
        }

        public Prompt Build()
        {
            _informacoes = _informacoes.TrimEnd(',', ' ');

            FormatarBasePrompt();

            Prompt promptFinal = new()
            {
                Mensagem = _basePrompt
                .Replace("[informações]", _informacoes)
            };

            return promptFinal;
        }

        public string FormatarBasePrompt()
        {
            return _basePrompt
                .Replace("[quantidadeMinimaDeExercicios]", _quantidadeMinimaDeExercicios.ToString())
                .Replace("[quantidadeMaximaDeExercicios]", _quantidadeMaximaDeExercicios.ToString());
        }
    }
}
