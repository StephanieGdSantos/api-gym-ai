using Xunit;
using Moq;
using System.Threading.Tasks;
using API.GymAi.Adapters;
using API.GymAi.Interfaces.Adapters;
using API.GymAi.Interfaces.Builders;
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
    public class TreinoBuilderTests
    {
        [Fact]
        public void ComVariacao_DeveAdicionarVariacaoNaLista()
        {
            // Arrange
            var builder = new TreinoBuilder();
            var variacao = new VariacaoDeTreino
            {
                Dia = "A",
                MusculosTrabalhados = new List<string> { "Peito" },
                Exercicio = new List<Exercicio>()
            };

            // Act
            builder.ComVariacao(variacao);
            var treino = builder.Build();

            // Assert
            Assert.Single(treino.VariacaoDeTreino);
            Assert.Equal("A", treino.VariacaoDeTreino[0].Dia);
        }

        [Fact]
        public void ComPeriodo_DeveDefinirPeriodoNoTreino()
        {
            // Arrange
            var builder = new TreinoBuilder();
            var periodo = new PeriodoTreino
            {
                DataInicio = "01/01/2024",
                DataFim = "31/01/2024"
            };

            // Act
            builder.ComPeriodo(periodo);
            var treino = builder.Build();

            // Assert
            Assert.NotNull(treino.Periodo);
            Assert.Equal("01/01/2024", treino.Periodo.DataInicio);
            Assert.Equal("31/01/2024", treino.Periodo.DataFim);
        }

        [Fact]
        public void Build_DeveRetornarTreinoComVariacoesEPeriodo()
        {
            // Arrange
            var builder = new TreinoBuilder();
            var variacao = new VariacaoDeTreino
            {
                Dia = "B",
                MusculosTrabalhados = new List<string> { "Costas" },
                Exercicio = new List<Exercicio>()
            };
            var periodo = new PeriodoTreino
            {
                DataInicio = "01/02/2024",
                DataFim = "28/02/2024"
            };

            // Act
            builder.ComVariacao(variacao).ComPeriodo(periodo);
            var treino = builder.Build();

            // Assert
            Assert.Single(treino.VariacaoDeTreino);
            Assert.Equal("B", treino.VariacaoDeTreino[0].Dia);
            Assert.Equal("01/02/2024", treino.Periodo.DataInicio);
            Assert.Equal("28/02/2024", treino.Periodo.DataFim);
        }
    }
}
