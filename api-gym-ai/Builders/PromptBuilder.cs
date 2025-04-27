using api_gym_ai.Interfaces.Builders;
using api_gym_ai.Models;
using System.Numerics;

namespace api_gym_ai.Builders
{
    public class PromptBuilder : IPromptBuilder
    {
        private string _basePrompt { get; set; } = "Haja como um personal trainer e considere as minhas informações: [informações]. Monte um plano de treino de academia no formato de variação [ABC, ABCD ou ABCDE], conforme indicado no campo 'variacaoTreino'. Cada treino deve conter entre 4 e 8 exercícios, dentro da duração indicada. Priorize a maior isolação possível dos grupos musculares — ou seja, quanto maior a quantidade de treinos, mais específicos e isolados devem ser os músculos trabalhados em cada dia, com ampla variedade de exercícios. Cada treino (A, B, C, etc.) deve conter exercícios suficientes para preencher aproximadamente o tempo de treino informado no campo 'tempoDeTreino' (por exemplo, se for informado 60 minutos, o treino A deve ter duração de 60 min, assim como o B, C, etc.). Para cada exercício, informe: nome do exercício, quantidade de séries, quantidade de repetições e músculos trabalhados no exercício. Siga o formato a seguir, preenchendo as máscaras sem colchetes: '[letra do treino]-[exercício 1], [séries]x[repetições], [músculo(s) alvo separados por espaço]_[exercício 2], [séries]x[repetições], [músculo(s) alvo separados por espaço]-[grupo(s) muscular(is) trabalhados no treino]|[letra do treino]-...' Os músculos alvo de cada exercício devem ser específicos (por exemplo: peito superior, tríceps lateral, dorsais, bíceps braquial, quadríceps, etc.). Após listar todos os exercícios de um treino (A, B, C...), informe também quais grupos musculares principais foram trabalhados no treino, separados por vírgula (não por exercício). Não adicione comentários, explicações, marcações de texto, markdown ou variações no formato. O retorno deve ser uma linha corrida. Siga estritamente o exemplo: 'TREINO A-remada baixa, 4x12, costas bíceps_supino na máquina, 3x15, peito tríceps-[superiores]|TREINO B-cadeira abdutora, 3x15, glúteos_cadeira flexora, 4x12, esquiotibiais-inferiores coxa e glúteos|TREINO C-...'";

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
            _informacoes += $"Tempo de Treino: {tempoDeTreino} minutos para cada variação, ";

            return this;
        }

        public IPromptBuilder ComVariacaoDeTreino(string variacao)
        {
            _informacoes += $"Variação de Treino: {variacao}, ";

            return this;
        }

        public IPromptBuilder ComVariacaoMuscular(string variacaoMuscular)
        {
            _informacoes += $"Variação Muscular: {variacaoMuscular}, ";

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
