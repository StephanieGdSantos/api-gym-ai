using api_gym_ai.Exceptions;
﻿using api_gym_ai.Interfaces.Adapters;
using api_gym_ai.Interfaces.Services;
using api_gym_ai.Models;
using System.Text.Json;

namespace api_gym_ai.Adapters
{
    public class RetornoChatAdapter : IRetornoChatAdapter
    {
        private readonly ICohereService _cohereService;
        private readonly IJsonAdapter _jsonAdapter;

        public RetornoChatAdapter(ICohereService cohereService, IJsonAdapter jsonAdapter)
        {
            _cohereService = cohereService;
            _jsonAdapter = jsonAdapter;
        }

        public async Task<string> ExtrairRespostaDoChat(Prompt prompt)
        {
            try
            {
                var response = await _cohereService.ChatAsync(prompt.Mensagem);

                var retornoChat = FormatarRetornoDoChat(response);

                return retornoChat;
            }
            catch (JsonException ex)
            {
                throw new JsonChatException("Erro ao processar o JSON da resposta.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao extrair a resposta do chat.", ex);
            }
        }

        private string FormatarRetornoDoChat(string retorno)
        {
            var respostaChat = _jsonAdapter.ExtrairMensagemDoChat(retorno);

            return respostaChat
                .Replace("\n", " ")
                .Replace("\r", " ")
                .Trim();
        }
    }
}
