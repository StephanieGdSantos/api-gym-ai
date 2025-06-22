using api_gym_ai.Interfaces.Adapters;
using api_gym_ai.Interfaces.Services;
using api_gym_ai.Models;
using System.Text.Json;

namespace api_gym_ai.Adapters
{
    public class RetornoChatAdapter : IRetornoChatAdapter
    {
        private readonly ICohereService _cohereService;

        public RetornoChatAdapter(ICohereService cohereService)
        {
            _cohereService = cohereService;
        }

        public async Task<string> ExtrairRespostaDoChat(Prompt prompt)
        {
            try
            {
                var retornoDoChat = await _cohereService.ChatAsync(prompt.Mensagem);

                var jsonRetornoDoChat = JsonSerializer.Deserialize<RetornoChat>(retornoDoChat);

                if (jsonRetornoDoChat == null || jsonRetornoDoChat.message == null || !jsonRetornoDoChat.message.content.Any())
                    throw new Exception("Resposta do chat inválida ou vazia.");

                var textoMensagemChat = jsonRetornoDoChat.message.content.First().text;

                var retornoChat = FormatarRetornoDoChat(textoMensagemChat);

                return retornoChat;
            }
            catch (JsonException ex)
            {
                throw new JsonException("Erro ao processar o JSON da resposta.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao extrair a resposta do chat.", ex);
            }
        }

        private string FormatarRetornoDoChat(string mensagemChat)
        {
            return mensagemChat
                .Replace("\n", " ")
                .Replace("\r", " ")
                .Trim();
        }
    }
}
