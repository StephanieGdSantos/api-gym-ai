using api_gym_ai.Interfaces.Builders;
using api_gym_ai.Models;

namespace api_gym_ai.Builders
{
    public class ExercicioBuilder : IExercicioBuilder
    {
        private Exercicio _exercicio = new();

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
            var exercicioConstruido = new Exercicio
            {
                Nome = _exercicio.Nome,
                Series = _exercicio.Series,
                Repeticoes = _exercicio.Repeticoes,
                MusculoAlvo = _exercicio.MusculoAlvo
            };

            _exercicio = new Exercicio();

            return exercicioConstruido;
        }
    }
}
