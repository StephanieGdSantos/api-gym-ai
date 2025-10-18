using APIGymAi.Models;

namespace APIGymAi.Adapters.Interface;

/// <summary>
/// Interface para adaptação de prompts.
/// </summary>
public interface IPromptAdapter
{
    /// <summary>
    /// Constrói um objeto <see cref="Prompt"/> com base nas informações de uma pessoa.
    /// </summary>
    /// <param name="pessoa">Objeto do tipo <see cref="Pessoa"/> contendo as informações necessárias.</param>
    /// <returns>Um objeto <see cref="Prompt"/> contendo a mensagem gerada.</returns>
    Prompt ConstruirPrompt(Pessoa pessoa);
}