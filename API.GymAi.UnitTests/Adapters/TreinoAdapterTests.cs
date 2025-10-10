using Xunit;
using Moq;
using System.Threading.Tasks;
using APIGymAi.Adapters;
using APIGymAi.Models;
using APIGymAi.Utils;
using System.Collections.Generic;
using APIGymAi.Facades;
using System.Text.Json;
using APIGymAi.Exceptions;
using System.Globalization;
using Microsoft.Extensions.Options;
using APIGymAi.Options;
using APIGymAi.Adapters.Interfaces;
using APIGymAi.Builders.Interfaces;

namespace APIGymAi.UnitTests.Adapters
{
    public class PromptBuilderTests
    {
        private readonly Mock<IPromptAdapter> _mockPromptAdapter;
        private readonly Mock<IRetornoChatAdapter> _mockRetornoChatAdapter;
        private readonly Mock<ITreinoBuilder> _mockTreinoBuilder;
        private readonly TreinoAdapter _treinoAdapter;

        public PromptBuilderTests()
        {
            var periodoDeTreinoOptions = new PeriodoDeTreinoOptions
            {
                Iniciante = 60,
                Intermediario = 75,
                Avancado = 45
            };

            var _mockPeriodoDeTreinoOptions = new Mock<IOptions<PeriodoDeTreinoOptions>>();
            _mockPeriodoDeTreinoOptions.Setup(x => x.Value).Returns(periodoDeTreinoOptions);

            _mockPromptAdapter = new Mock<IPromptAdapter>();
            _mockRetornoChatAdapter = new Mock<IRetornoChatAdapter>();
            _mockTreinoBuilder = new Mock<ITreinoBuilder>();

            _treinoAdapter = new TreinoAdapter(
                _mockPromptAdapter.Object,
                _mockRetornoChatAdapter.Object,
                _mockTreinoBuilder.Object,
                _mockPeriodoDeTreinoOptions.Object
            );
        }

        [Fact]
        public async Task MontarTreino_DeveRetornarTreino_QuandoPessoaForValido()
        {
            // Arrange  
            var pessoa = new Pessoa
            {
                Idade = 25,
                Altura = 1.75,
                Peso = 70,
                InfoPreferencias = new InfoPreferencias
                {
                    Nivel = InfoPreferencias.EnumNivelCondicionamento.Iniciante,
                    Objetivo = InfoPreferencias.EnumObjetivo.Hipertrofia
                }
            };

            var prompt = new Prompt { Mensagem = "Mocked Prompt" };
            var retornoChat = "{\"variacaoDeTreino\": [{\"dia\": \"Segunda\", \"musculosTrabalhados\": [\"Peito\"], \"exercicios\": []}]}";
            var treinoProposto = new Treino
            {
                VariacaoDeTreino = new List<VariacaoDeTreino>
               {
                   new VariacaoDeTreino
                   {
                       Dia = "Segunda",
                       MusculosTrabalhados = new List<string> { "Peito" },
                       Exercicio = new List<Exercicio>()
                   }
               }
            };

            _mockPromptAdapter.Setup(x => x.ConstruirPrompt(pessoa)).Returns(prompt);
            _mockRetornoChatAdapter.Setup(x => x.ExtrairRespostaDoChat(prompt)).ReturnsAsync(retornoChat);
            _mockTreinoBuilder.Setup(x => x.ComVariacao(It.IsAny<VariacaoDeTreino>())).Returns(_mockTreinoBuilder.Object);
            _mockTreinoBuilder.Setup(x => x.ComPeriodo(It.IsAny<PeriodoTreino>())).Returns(_mockTreinoBuilder.Object);
            _mockTreinoBuilder.Setup(x => x.Build()).Returns(treinoProposto);

            // Act  
            var result = await _treinoAdapter.MontarTreino(pessoa);

            // Assert  
            Assert.NotNull(result);
            Assert.Equal(1, result.VariacaoDeTreino.Count);
            Assert.Equal("Segunda", result.VariacaoDeTreino[0].Dia);
            Assert.Contains("Peito", result.VariacaoDeTreino[0].MusculosTrabalhados);
        }

        [Fact]
        public async Task MontarTreino_DeveRetornarJsonDeserializeException_QuandoARespostaForInvalida()
        {
            // Arrange  
            var pessoa = new Pessoa();
            var prompt = new Prompt { Mensagem = "Mocked Prompt" };
            var retornoInvalido = "Invalid JSON";

            _mockPromptAdapter.Setup(x => x.ConstruirPrompt(pessoa)).Returns(prompt);
            _mockRetornoChatAdapter.Setup(x => x.ExtrairRespostaDoChat(prompt)).ReturnsAsync(retornoInvalido);

            // Act & Assert  
            await Assert.ThrowsAsync<JsonDeserializationException>(() => _treinoAdapter.MontarTreino(pessoa));
        }

        [Fact]
        public async Task MontarTreino_DeveLancarArgumentNullException_QuandoPessoaForNula()
        {
            // Arrange  
            Pessoa pessoa = null;
            // Act & Assert  
            await Assert.ThrowsAsync<ArgumentNullException>(() => _treinoAdapter.MontarTreino(pessoa));
        }

        [Fact]
        public void CalcularPeriodoDeTreino_DeveRetornarPeriodoValidoDeAcordoComONivel()
        {
            //Arrange
            var nivelPessoa = InfoPreferencias.EnumNivelCondicionamento.Iniciante;
            var periodoEsperadoEmDias = 60;
            var formatoDeData = CultureInfo.GetCultureInfo("pt-BR").DateTimeFormat;

            //Act
            var periodoDeTreino = _treinoAdapter.CalcularPeriodoDeTreino(nivelPessoa);
            var periodoRetornadoEmDias = DateTime.Parse(periodoDeTreino.DataFim, formatoDeData) - 
                DateTime.Parse(periodoDeTreino.DataInicio, formatoDeData);

            //Assert
            Assert.Equal(periodoEsperadoEmDias, periodoRetornadoEmDias.Days);
        }
    }
}
