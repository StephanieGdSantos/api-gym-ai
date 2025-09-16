using API.GymAi.Models;

namespace API.GymAi.Builders.Interfaces
{
    /// <summary>
    /// Interface para construção de objetos do tipo Treino.
    /// </summary>
    public interface ITreinoBuilder
    {
        /// <summary>
        /// Define a variação de treino.
        /// </summary>
        /// <param name="variacaoDeTreino">A variação de treino a ser configurada.</param>
        /// <returns>O próprio construtor para encadeamento de chamadas.</returns>
        ITreinoBuilder ComVariacao(VariacaoDeTreino variacaoDeTreino);

        /// <summary>
        /// Define o período do treino.
        /// </summary>
        /// <param name="periodo">O período do treino a ser configurado.</param>
        /// <returns>O próprio construtor para encadeamento de chamadas.</returns>
        ITreinoBuilder ComPeriodo(PeriodoTreino periodo);

        /// <summary>
        /// Constrói e retorna o objeto Treino configurado.
        /// </summary>
        /// <returns>O objeto Treino configurado.</returns>
        Treino Build();
    }
}
