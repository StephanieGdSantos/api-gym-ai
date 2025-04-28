using api_gym_ai.Facades;
using api_gym_ai.Interfaces.Builders;
using api_gym_ai.Models;
using Moq;

namespace api_gym_ai.Tests
{
    public class ExercicioAdapterTests
    {
        private readonly Mock<IExercicioBuilder> _mockExercicioBuilder;
        private readonly ExercicioAdapter _exercicioAdapter;

        public ExercicioAdapterTests()
        {
            _mockExercicioBuilder = new Mock<IExercicioBuilder>();
            _exercicioAdapter = new ExercicioAdapter(_mockExercicioBuilder.Object);
        }

        [Fact]
        public void ListarExerciciosPropostos_QuandoOTreinoForValido_DeveRetornarListaDeExercicios()
        {
            // Arrange
            string treinoProposto = "Agachamento,3x12,Quadriceps Gluteos_Prancha,2x30s,Abdomen";
            _mockExercicioBuilder
                .Setup(b => b.ComNome(It.IsAny<string>()))
                .Returns(_mockExercicioBuilder.Object);
            _mockExercicioBuilder
                .Setup(b => b.ComSeries(It.IsAny<int>()))
                .Returns(_mockExercicioBuilder.Object);
            _mockExercicioBuilder
                .Setup(b => b.ComRepeticoes(It.IsAny<string>()))
                .Returns(_mockExercicioBuilder.Object);
            _mockExercicioBuilder
                .Setup(b => b.ComMusculosAlvo(It.IsAny<IEnumerable<string>>()))
                .Returns(_mockExercicioBuilder.Object);
            _mockExercicioBuilder
                .Setup(b => b.Build())
                .Returns(new Exercicio());

            // Act
            var result = _exercicioAdapter.ListarExerciciosPropostos(treinoProposto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void ListarExerciciosPropostos_QuandoOtreinoPropostoForInvalido_DeveRetornarListaVazia()
        {
            // Arrange
            string treinoProposto = "InvalidInput";

            // Act
            var result = _exercicioAdapter.ListarExerciciosPropostos(treinoProposto);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void ListarExerciciosPropostos_QuandoOTreinoPropostoForVazio_DeveRetornarListaVazia()
        {
            // Arrange
            string treinoProposto = "";

            // Act
            var result = _exercicioAdapter.ListarExerciciosPropostos(treinoProposto);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void ConstruirExercicio_QuandoParametrosForemvalidos_DeveRetornarExercicio()
        {
            // Arrange
            string nome = "Agachamento";
            int series = 3;
            string repeticoes = "12";
            var musculosAlvo = new List<string> { "Quadriceps", "Gluteos" };

            _mockExercicioBuilder
                .Setup(b => b.ComNome(nome))
                .Returns(_mockExercicioBuilder.Object);
            _mockExercicioBuilder
                .Setup(b => b.ComSeries(series))
                .Returns(_mockExercicioBuilder.Object);
            _mockExercicioBuilder
                .Setup(b => b.ComRepeticoes(repeticoes))
                .Returns(_mockExercicioBuilder.Object);
            _mockExercicioBuilder
                .Setup(b => b.ComMusculosAlvo(musculosAlvo))
                .Returns(_mockExercicioBuilder.Object);
            _mockExercicioBuilder
                .Setup(b => b.Build())
                .Returns(new Exercicio());

            // Act
            var result = _exercicioAdapter.ConstruirExercicio(nome, series, repeticoes, musculosAlvo);

            // Assert
            Assert.NotNull(result);
        }
    }
}