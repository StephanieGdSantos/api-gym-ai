namespace APIGymAi.Repositories.Interface;

/// <summary>
/// Interface para o repositório de chat, responsável por enviar prompts e obter respostas.
/// </summary>
public interface IChatRepository
{
    /// <summary>
    /// Envia um prompt de texto e retorna a resposta como uma string.
    /// </summary>
    /// <param name="prompt">O prompt de texto a ser enviado.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. O resultado contém a resposta como uma string.</returns>
    Task<string> SendAsync(string prompt);
}
