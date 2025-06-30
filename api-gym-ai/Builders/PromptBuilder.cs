using api_gym_ai.Interfaces.Builders;
using api_gym_ai.Models;
using System.Numerics;
using static api_gym_ai.Models.InfoPreferencias;

namespace api_gym_ai.Builders
{
    public class PromptBuilder : IPromptBuilder
    {
        private const int _quantidadeMinimaDeExercicios = 4;
        private const int _quantidadeMaximaDeExercicios = 12;
        private string _basePrompt { get; set; } = "Haja como um personal trainer profissional. Considere as " +
            "seguintes informações do aluno: [informações]. Monte um plano de treino de academia no formato de " +
            "variação indicado no campo 'variacaoTreino' (ABC, ABCD ou ABCDE). Cada treino deve conter entre " +
            $"{_quantidadeMinimaDeExercicios} e {_quantidadeMaximaDeExercicios} exercícios, com duração aproximada " +
            "de 'tempoDeTreino' por dia. Quanto mais treinos, mais isolados " +
            "e específicos devem ser os grupos musculares por dia.\r\n\r\nPara cada exercício, informe:\r\n\r\n" +
            "nome do exercício\r\n\r\nnúmero de séries\r\n\r\nnúmero de repetições\r\n\r\nmúsculos alvo " +
            "(ex: “bíceps braquial”, “peitoral superior”, “glúteo máximo”)\r\n\r\nSua resposta deve ser um JSON " +
            "válido, no formato exato abaixo. Não adicione comentários, explicações, formatação markdown, nem texto " +
            "adicional. Apenas o JSON. json {\"variacaoDeTreino\": [{\"dia\": \"TREINO A\",\"musculosTrabalhados\": " +
            "[\"peitoral\", \"tríceps\"],\"exercicios\": [{\"nome\": \"supino reto com barra\", \"series\": 4, " +
            "\"repeticoes\": \"10-12\", \"musculoAlvo\": [\"peitoral médio\", \"tríceps medial\" ]} " +
            "// demais exercícios ]}// TREINO B, C, etc.]}";

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

            Prompt promptFinal = new()
            {
                Mensagem = _basePrompt
                .Replace("[informações]", _informacoes)
            };

            return promptFinal;
        }
    }
}
