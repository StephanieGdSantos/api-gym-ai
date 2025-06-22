using api_gym_ai.Builders;
using api_gym_ai.Interfaces.Adapters;
using api_gym_ai.Interfaces.Builders;
using api_gym_ai.Models;

namespace api_gym_ai.Facades
{
    public class TreinoAdapter : ITreinoAdapter
    {
        private readonly IPromptAdapter _promptAdapter;
        private readonly IVariacaoDeTreinoAdapter _variacaoDeTreinoAdapter;
        private readonly ITreinoBuilder _treinoBuilder;

        public TreinoAdapter(IPromptAdapter promptAdapter, IVariacaoDeTreinoAdapter variacaoDeTreinoAdapter, ITreinoBuilder treinoBuilder)
        {
            _promptAdapter = promptAdapter;
            _variacaoDeTreinoAdapter = variacaoDeTreinoAdapter;
            _treinoBuilder = treinoBuilder;
        }

        public async Task<Treino?> MontarTreino(Pessoa pessoa)
        {
            var prompt = _promptAdapter.ConstruirPrompt(pessoa);
            var treinoProposto = await _retornoChatAdapter.ExtrairRespostaDoChat(prompt);

            var variacaoDeTreino = _variacaoDeTreinoAdapter.ListarVariacaoDeTreinos(treinoProposto);
            var quantidadeVariacoes = variacaoDeTreino.Count;

            switch (quantidadeVariacoes)
            {
                case 2:
                    return _treinoBuilder
                        .ComVariacaoA(variacaoDeTreino[0])
                        .ComVariacaoB(variacaoDeTreino[1])
                        .ComDataInicio(DateTime.Now)
                        .ComDataFim(DateTime.Now.AddDays(120))
                        .Build();
                case 3:
                    return _treinoBuilder
                        .ComVariacaoA(variacaoDeTreino[0])
                        .ComVariacaoB(variacaoDeTreino[1])
                        .ComVariacaoC(variacaoDeTreino[2])
                        .ComDataInicio(DateTime.Now)
                        .ComDataFim(DateTime.Now.AddDays(120))
                        .Build();
                case 4:
                    return _treinoBuilder
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
