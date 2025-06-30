using Xunit;
using Moq;
using api_gym_ai.Interfaces.Adapters;
using api_gym_ai.Interfaces.Builders;
using api_gym_ai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using api_gym_ai.Facades;

namespace Tests.Adapters
{
    public class PromptAdapterTests
    {
        private readonly Mock<IPromptBuilder> _mockPromptBuilder;
        private readonly IPromptAdapter _promptAdapter;

        public PromptAdapterTests()
        {
            _mockPromptBuilder = new Mock<IPromptBuilder>();
            _promptAdapter = new PromptAdapter(_mockPromptBuilder.Object);
        }

        [Fact]
        public void ConstruirPrompt_DeveLancarArgumentNullException_QuandoPessoaForNula()
        {
            // Act & Assert  
            Assert.Throws<ArgumentNullException>(() => _promptAdapter.ConstruirPrompt(null));
        }

        [Fact]
        public void ConstruirPrompt_DeveRetornarPrompt_QuandoPessoaForValida()
        {
            // Arrange  
            var pessoa = new Pessoa
            {
                Idade = 25,
                Altura = 1.75,
                Peso = 70,
                InfoCorporais = new InfoCorporais
                {
                    MassaMuscular = 30,
                    PercentualGordura = 15,
                    Limitacoes = new List<string> { "Lesão no joelho" }
                },
                InfoPreferencias = new InfoPreferencias
                {
                    Objetivo = InfoPreferencias.EnumObjetivo.Hipertrofia,
                    PartesDoCorpoEmFoco = new List<InfoPreferencias.EnumPartesDoCorpoEmFoco>
                   {
                       InfoPreferencias.EnumPartesDoCorpoEmFoco.Peito,
                       InfoPreferencias.EnumPartesDoCorpoEmFoco.Bracos
                   },
                    TempoDeTreinoEmMinutos = 60,
                    VariacaoTreino = "Alta",
                    Observacao = "Treino focado em força",
                    Nivel = InfoPreferencias.EnumNivelCondicionamento.Intermediario
                }
            };

            var resultadoEsperado = new Prompt { Mensagem = "Prompt gerado com sucesso" };

            _mockPromptBuilder.Setup(pb => pb.ComIdade(It.IsAny<string>())).Returns(_mockPromptBuilder.Object);
            _mockPromptBuilder.Setup(pb => pb.ComPeso(It.IsAny<string>())).Returns(_mockPromptBuilder.Object);
            _mockPromptBuilder.Setup(pb => pb.ComAltura(It.IsAny<string>())).Returns(_mockPromptBuilder.Object);
            _mockPromptBuilder.Setup(pb => pb.ComMassaMuscular(It.IsAny<string>())).Returns(_mockPromptBuilder.Object);
            _mockPromptBuilder.Setup(pb => pb.ComPercentualDeGordura(It.IsAny<string>())).Returns(_mockPromptBuilder.Object);
            _mockPromptBuilder.Setup(pb => pb.ComLimitacoes(It.IsAny<string>())).Returns(_mockPromptBuilder.Object);
            _mockPromptBuilder.Setup(pb => pb.ComPartesDoCorpoEmFoco(It.IsAny<string>())).Returns(_mockPromptBuilder.Object);
            _mockPromptBuilder.Setup(pb => pb.ComObjetivo(It.IsAny<string>())).Returns(_mockPromptBuilder.Object);
            _mockPromptBuilder.Setup(pb => pb.ComTempoDeTreinoEmMinutos(It.IsAny<string>())).Returns(_mockPromptBuilder.Object);
            _mockPromptBuilder.Setup(pb => pb.ComVariacaoDeTreino(It.IsAny<string>())).Returns(_mockPromptBuilder.Object);
            _mockPromptBuilder.Setup(pb => pb.ComObservacao(It.IsAny<string>())).Returns(_mockPromptBuilder.Object);
            _mockPromptBuilder.Setup(pb => pb.ComNivel(It.IsAny<string>())).Returns(_mockPromptBuilder.Object);
            _mockPromptBuilder.Setup(pb => pb.Build()).Returns(resultadoEsperado);

            // Act  
            var resultado = _promptAdapter.ConstruirPrompt(pessoa);

            // Assert  
            Assert.NotNull(resultado);
            Assert.Equal(resultadoEsperado.Mensagem, resultado.Mensagem);
        }
    }
}
