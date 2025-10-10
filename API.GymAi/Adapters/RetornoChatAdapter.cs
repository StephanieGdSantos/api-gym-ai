using APIGymAi.Adapters.Interface;
using APIGymAi.Models;
using APIGymAi.Services.Interface;
using APIGymAi.Utils;

namespace APIGymAi.Adapters;

/// <summary>  
/// Adapter responsável por processar o retorno do chat.  
/// </summary>  
public class RetornoChatAdapter(IChatService cohereService) : IRetornoChatAdapter
{
    private readonly IChatService _cohereService = cohereService;

    /// <summary>  
    /// Extrai a resposta do chat com base no prompt fornecido.  
    /// </summary>  
    /// <param name="prompt">O prompt contendo a mensagem para o chat.</param>  
    /// <returns>Uma string formatada com a resposta do chat.</returns>  
    public async Task<string> ExtrairRespostaDoChat(Prompt prompt)
    {
        try
        {
            var retornoDoChat = await _cohereService.ChatAsync(prompt.Mensagem);

            var jsonRetornoDoChat = JsonUtils.DeserializeOrThrow<RetornoChat>(retornoDoChat, nameof(RetornoChat));

            var textoMensagemChat = jsonRetornoDoChat.message.content.First().text;

            var retornoChat = FormatarRetornoDoChat(textoMensagemChat);

            return retornoChat;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao extrair a resposta do chat.", ex);
        }
    }

    /// <summary>  
    /// Formata a mensagem do chat removendo quebras de linha e espaços desnecessários.  
    /// </summary>  
    /// <param name="mensagemChat">A mensagem do chat a ser formatada.</param>  
    /// <returns>Uma string formatada.</returns>  
    public string FormatarRetornoDoChat(string mensagemChat)
    {
        return mensagemChat
            .Replace("\n", " ")
            .Replace("\r", " ")
            .Trim();
    }
}
