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
    }
}