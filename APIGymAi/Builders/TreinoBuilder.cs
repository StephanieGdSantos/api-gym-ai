using APIGymAi.Builders.Interface;
using APIGymAi.Models;

namespace APIGymAi.Builders;

/// <summary>
/// Builder para criar instâncias de Treino.
/// </summary>
public class TreinoBuilder : ITreinoBuilder
{
    private readonly Treino _treino = new();

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="TreinoBuilder"/>.
    /// </summary>
    public TreinoBuilder()
    {
        _treino.VariacaoDeTreino = new List<VariacaoDeTreino>();
    }

    /// <summary>
    /// Adiciona uma variação de treino ao objeto <see cref="Treino"/>.
    /// </summary>
    /// <param name="variacaoDeTreino">A variação de treino a ser adicionada.</param>
    /// <returns>O próprio <see cref="ITreinoBuilder"/> para encadeamento.</returns>
    public ITreinoBuilder ComVariacao(VariacaoDeTreino variacaoDeTreino)
    {
        _treino.VariacaoDeTreino.Add(variacaoDeTreino);
        return this;
    }

    /// <summary>
    /// Define o período do treino.
    /// </summary>
    /// <param name="periodo">O período do treino.</param>
    /// <returns>O próprio <see cref="ITreinoBuilder"/> para encadeamento.</returns>
    public ITreinoBuilder ComPeriodo(PeriodoTreino periodo)
    {
        _treino.Periodo = periodo;
        return this;
    }

    /// <summary>
    /// Constrói e retorna o objeto <see cref="Treino"/> configurado.
    /// </summary>
    /// <returns>O objeto <see cref="Treino"/> configurado.</returns>
    public Treino Build()
    {
        return _treino;
    }
}
