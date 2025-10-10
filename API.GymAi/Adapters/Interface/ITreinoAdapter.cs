using APIGymAi.Models;

namespace APIGymAi.Adapters.Interface;

/// <summary>
/// Interface responsável por adaptar e montar treinos personalizados para uma pessoa.
/// </summary>
public interface ITreinoAdapter
{
    /// <summary>
    /// Monta um treino personalizado com base nas características de uma pessoa.
    /// </summary>
    /// <param name="pessoa">Objeto que contém as informações da pessoa para quem o treino será montado.</param>
    /// <returns>Um objeto <see cref="Treino"/> contendo o treino montado ou null se não for possível montar.</returns>
    public Task<Treino?> MontarTreino(Pessoa pessoa);
}
