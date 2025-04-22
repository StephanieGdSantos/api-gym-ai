using api_gym_ai.Interfaces;
using api_gym_ai.Models;

namespace api_gym_ai.Builders
{
    public class PromptBuilder : IPromptBuilder
    {
        private string _basePrompt { get; set; } = "Haja como um personal trainer e considere as seguintes informações: [informações]. A partir dessas informações recomende um plano de exercícios para fazer na academia. me responda utilizando especificamente o formato '[exercício 1], [séries]x[repetições], [músculo(s) alvo separados por espaço]\n [exercício 2], [séries]x[repetições], [músculo(s) alvo separados por espaço]', onde os itens com chaves são mascaras para serem preenchidas; exclua as chaves do texto.não quero mais informações além das pedidas. a mensagem retornada devem seguir exatamente o modelo passado";

        private string _informacoes { get; set; } = string.Empty;

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

        public IPromptBuilder ComTempoDeTreino(string tempoDeTreino)
        {
            _informacoes += $"Tempo de Treino: {tempoDeTreino}, ";
            return this;
        }

        public IPromptBuilder ComVariacaoDeTreino(string variacao)
        {
            _informacoes += $"Variação de Treino: {variacao}, ";
            return this;
        }

        public Prompt Build()
        {
            _informacoes = _informacoes.TrimEnd(',', ' ');

            Prompt promptFinal = new()
            {
                Mensagem = _basePrompt
                .Replace("[informações]", _informacoes)
            };

            return promptFinal;
        }
    }
}
