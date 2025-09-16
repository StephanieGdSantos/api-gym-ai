namespace API.GymAi.Services.Interface
{
    /// <summary>
    /// Interface para o serviço de chat.
    /// </summary>
    public interface IChatService
    {
        /// <summary>
        /// Envia um prompt para o serviço de chat e retorna a resposta.
        /// </summary>
        /// <param name="prompt">O prompt a ser enviado.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona. O resultado contém a resposta do chat.</returns>
        Task<string> ChatAsync(string prompt);
    }
}
