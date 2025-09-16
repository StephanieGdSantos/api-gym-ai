using API.GymAi.Models;

namespace API.GymAi.Adapters.Interfaces
{
    /// <summary>
    /// Interface para adaptação de retorno do chat.
    /// </summary>
    public interface IRetornoChatAdapter
    {
        /// <summary>
        /// Extrai a resposta do chat com base no prompt fornecido.
        /// </summary>
        /// <param name="prompt">O prompt contendo a mensagem para o chat.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona. O resultado contém a resposta do chat.</returns>
        public Task<string> ExtrairRespostaDoChat(Prompt prompt);

        /// <summary>
        /// Formata o retorno do chat em uma string legível.
        /// </summary>
        /// <param name="mensagemChat">A mensagem retornada pelo chat.</param>
        /// <returns>Uma string formatada.</returns>
        public string FormatarRetornoDoChat(string mensagemChat);
    }
}
