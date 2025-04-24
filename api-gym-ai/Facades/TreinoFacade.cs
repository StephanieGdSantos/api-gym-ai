using api_gym_ai.Builders;
using api_gym_ai.Models;

namespace api_gym_ai.Facades
{
    public static class TreinoFacade
    {
        public static Treino? MontarTreino(string retornoChat)
        {
            var treinoProposto = PromptFacade.ExtrairRespostaDoChat(retornoChat);
            var variacaoDeTreino = VariacaoDeTreinoFacade.ListarVariacaoDeTreinos(treinoProposto);

            var quantidadeVariacoes = variacaoDeTreino.Count;

            switch (quantidadeVariacoes)
            {
                case 2:
                    return new TreinoBuilder()
                        .ComVariacaoA(variacaoDeTreino[0])
                        .ComVariacaoB(variacaoDeTreino[1])
                        .ComDataInicio(DateTime.Now)
                        .ComDataFim(DateTime.Now.AddDays(120))
                        .Build();
                case 3:
                    return new TreinoBuilder()
                        .ComVariacaoA(variacaoDeTreino[0])
                        .ComVariacaoB(variacaoDeTreino[1])
                        .ComVariacaoC(variacaoDeTreino[2])
                        .ComDataInicio(DateTime.Now)
                        .ComDataFim(DateTime.Now.AddDays(120))
                        .Build();
                case 4:
                    return new TreinoBuilder()
                        .ComVariacaoA(variacaoDeTreino[0])
                        .ComVariacaoB(variacaoDeTreino[1])
                        .ComVariacaoC(variacaoDeTreino[2])
                        .ComVariacaoD(variacaoDeTreino[3])
                        .ComDataInicio(DateTime.Now)
                        .ComDataFim(DateTime.Now.AddDays(120))
                        .Build();
            }

            return null;
        }
    }
}
