using API.GymAi.Adapters.Interfaces;
using API.GymAi.Models;
using API.GymAi.Services.Interface;
using API.GymAi.Utils;
using System.Text.Json;

namespace API.GymAi.Adapters
{
    public class RetornoChatAdapter : IRetornoChatAdapter
    {
        private readonly IChatService _cohereService;

        public RetornoChatAdapter(IChatService cohereService)
        {
            _cohereService = cohereService;
        }

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

        public string FormatarRetornoDoChat(string mensagemChat)
        {
            return mensagemChat
                .Replace("\n", " ")
                .Replace("\r", " ")
                .Trim();
        }
    }
}
