using System.Text.Json;

namespace api_gym_ai.Facades
{
    public static class PromptFacade
    {
        public static string ExtrairRespostaDoChat(string retornoChat)
        {
            var conteudoJson = JsonDocument.Parse(retornoChat);
            var root = conteudoJson.RootElement;
            var conteudoGeral = root.GetProperty("message").GetProperty("content");
            var respostaChat = conteudoGeral[0].GetProperty("text").GetString();

            if (respostaChat == null)
            {
                throw new Exception("Erro ao extrair a resposta do chat.");
            }
            return respostaChat;
        }
    }
}
