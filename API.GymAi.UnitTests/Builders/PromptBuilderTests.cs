using Xunit;
using Moq;
using System.Threading.Tasks;
using API.GymAi.Adapters;
using API.GymAi.Models;
using API.GymAi.Utils;
using System.Collections.Generic;
using API.GymAi.Facades;
using System.Text.Json;
using API.GymAi.Exceptions;
using System.Globalization;
using Microsoft.Extensions.Options;
using API.GymAi.Options;
using API.GymAi.Builders;

namespace API.GymAi.UnitTests.Builders
{
    public class PromptBuilderTests
    {
        private Mock<IOptions<InformacoesPromptOptions>> _mockInformacoesPromptOptions;

        public PromptBuilderTests()
        {
            var basePrompt = "Haja como um personal trainer profissional. Considere as seguintes informações do aluno: [informações]. " +
                "Monte um plano de treino de academia no formato de variação indicado no campo 'variacaoTreino' (ABC, ABCD ou ABCDE). " +
                "Cada treino deve conter entre [quantidadeMinimaDeExercicios] e [quantidadeMaximaDeExercicios] exercícios, com duração " +
                "aproximada de 'tempoDeTreino' por dia. Quanto mais treinos, mais isolados e específicos devem ser os grupos musculares " +
                "por dia.\r\n\r\nPara cada exercício, informe:\r\n\r\n nome do exercício\r\n\r\nnúmero de séries\r\n\r\nnúmero de " +
                "repetições\r\n\r\nmúsculos alvo (ex: “bíceps braquial”, “peitoral superior”, “glúteo máximo”)\r\n\r\nSua resposta " +
                "deve ser um JSON válido, no formato exato abaixo. Não adicione comentários, explicações, formatação markdown, nem texto " +
                "adicional. Apenas o JSON. json {\"variacaoDeTreino\": [{\"dia\": \"TREINO A\",\"musculosTrabalhados\": [\"peitoral\", " +
                "\"tríceps\"],\"exercicios\": [{\"nome\": \"supino reto com barra\", \"series\": 4, \"repeticoes\": \"10-12\", " +
                "\"musculoAlvo\": [\"peitoral médio\", \"tríceps medial\" ]} // demais exercícios ]}// TREINO B, C, etc.]}";

            _mockInformacoesPromptOptions = new Mock<IOptions<InformacoesPromptOptions>>();
            _mockInformacoesPromptOptions.SetupGet(o => o.Value).Returns(new InformacoesPromptOptions
            {
                BasePrompt = basePrompt,
                QuantidadeMinimaExercicios = 1,
                QuantidadeMaximaExercicios = 5
            });
        }

        [Fact]
        public void ComAltura_DeveAdicionarAlturaAoPrompt()
        {
            var builder = new PromptBuilder(_mockInformacoesPromptOptions.Object);
            builder.ComAltura("1.80");
            var prompt = builder.Build();

            Assert.Contains("Altura: 1.80", prompt.Mensagem);
        }

        [Fact]
        public void ComIdade_DeveAdicionarIdadeAoPrompt()
        {
            var builder = new PromptBuilder(_mockInformacoesPromptOptions.Object);
            builder.ComIdade("25");
            var prompt = builder.Build();

            Assert.Contains("Idade: 25", prompt.Mensagem);
        }

        [Fact]
        public void ComLimitacoes_NaoAdicionaQuandoNuloOuVazio()
        {
            var builder = new PromptBuilder(_mockInformacoesPromptOptions.Object);
            builder.ComLimitacoes(null);
            builder.ComLimitacoes("");
            var prompt = builder.Build();

            Assert.DoesNotContain("Limitações:", prompt.Mensagem);
        }

        [Fact]
        public void ComLimitacoes_AdicionaQuandoNaoVazio()
        {
            var builder = new PromptBuilder(_mockInformacoesPromptOptions.Object);
            builder.ComLimitacoes("Joelho");
            var prompt = builder.Build();

            Assert.Contains("Limitações: Joelho", prompt.Mensagem);
        }

        [Fact]
        public void Build_DeveSubstituirInformacoesNoPrompt()
        {
            var builder = new PromptBuilder(_mockInformacoesPromptOptions.Object);

            builder
                .ComAltura("1.75")
                .ComIdade("30")
                .ComPeso("80");

            var prompt = builder.Build();

            Assert.Contains("Altura: 1.75", prompt.Mensagem);
            Assert.Contains("Idade: 30", prompt.Mensagem);
            Assert.Contains("Peso: 80", prompt.Mensagem);
            Assert.DoesNotContain("[informações]", prompt.Mensagem);
        }

        [Fact]
        public void Build_DeveRemoverVirgulaFinal()
        {
            var builder = new PromptBuilder(_mockInformacoesPromptOptions.Object);
            builder.ComAltura("1.70").ComIdade("28");
            var prompt = builder.Build();

            Assert.DoesNotMatch(@",\s*$", prompt.Mensagem);
        }
    }
}
