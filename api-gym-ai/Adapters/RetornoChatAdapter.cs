using api_gym_ai.Interfaces.Adapters;
using api_gym_ai.Interfaces.Services;
using api_gym_ai.Models;
using api_gym_ai.Utils;
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

                var jsonRetornoDoChat = JsonUtils.DeserializeOrThrow<RetornoChat>(retornoDoChat, nameof(RetornoChat));

                if (jsonRetornoDoChat == null || jsonRetornoDoChat.message == null || !jsonRetornoDoChat.message.content.Any())
                    throw new Exception("Resposta do chat inválida ou vazia.");

                var textoMensagemChat = jsonRetornoDoChat.message.content.First().text;

                var retornoChat = FormatarRetornoDoChat(textoMensagemChat);

                return retornoChat;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao extrair a resposta do chat.", ex);
            }
        }

        public string FormatarRetornoDoChat(string mensagemChat)
        {
            return mensagemChat
                .Replace("\n", " ")
                .Replace("\r", " ")
                .Trim();
        }
    }
}
