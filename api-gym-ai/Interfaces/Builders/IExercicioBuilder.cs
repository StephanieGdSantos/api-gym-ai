using api_gym_ai.Models;

namespace api_gym_ai.Interfaces.Builders
{
    public interface IExercicioBuilder
    {
        /// <summary>
        /// Constrói o nome do exercício.
        /// </summary>
        /// <param name="nome">Nome do exercício.</param>
        /// <returns>Builder do exercício</returns>
        public IExercicioBuilder ComNome(string nome);

        /// <summary>
        /// Constrói a quantidade de séries.
        /// </summary>
        /// <param name="series">Quantidade de séries.</param>
        /// <returns>Builder do exercício</returns>
        public IExercicioBuilder ComSeries(int series);

        /// <summary>
        /// Constrói a quantidade de repetições.
        /// </summary>
        /// <param name="repeticoes">Quantidade de repetições.</param>
        /// <returns>Builder do exercício</returns>
        public IExercicioBuilder ComRepeticoes(string repeticoes);

        /// <summary>
        /// Constrói os músculos alvo.
        /// </summary>
        /// <param name="musculosAlvo">Músculos alvo.</param>
        /// <returns>Builder do exercício</returns>
        public IExercicioBuilder ComMusculosAlvo(IEnumerable<string> musculosAlvo);

        /// <summary>
        /// Constrói o objeto exercício.
        /// </summary>
        /// <returns>Exercício construído</returns>
        public Exercicio Build();
    }
}