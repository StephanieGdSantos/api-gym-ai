using api_gym_ai.Interfaces;
using api_gym_ai.Models;

namespace api_gym_ai.Builders
{
    public class ExercicioBuilder : IExercicioBuilder
    {
        private readonly Exercicio _exercicio = new();

        public IExercicioBuilder ComMusculosAlvo(IEnumerable<string> musculosAlvo)
        {
            _exercicio.MusculoAlvo = musculosAlvo;
            return this;
        }

        public IExercicioBuilder ComNome(string nome)
        {
            _exercicio.Nome = nome;
            return this;
        }

        public IExercicioBuilder ComRepeticoes(string repeticoes)
        {
            _exercicio.Repeticoes = repeticoes;
            return this;
        }

        public IExercicioBuilder ComSeries(int series)
        {
            _exercicio.Series = series;
            return this;
        }

        public Exercicio Build()
        {
            return _exercicio;
        }
    }
}
