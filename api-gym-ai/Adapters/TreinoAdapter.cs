using api_gym_ai.Builders;
using api_gym_ai.Interfaces.Adapters;
using api_gym_ai.Interfaces.Builders;
using api_gym_ai.Models;

namespace api_gym_ai.Facades
{
    public class TreinoAdapter : ITreinoAdapter
    {
        private readonly IPromptAdapter _promptAdapter;
        private readonly IRetornoChatAdapter _retornoChatAdapter;
        private readonly IVariacaoDeTreinoAdapter _variacaoDeTreinoAdapter;
        private readonly ITreinoBuilder _treinoBuilder;

        public TreinoAdapter(IPromptAdapter promptAdapter, IRetornoChatAdapter retornoChatAdapter, IVariacaoDeTreinoAdapter variacaoDeTreinoAdapter, ITreinoBuilder treinoBuilder)
        {
            _promptAdapter = promptAdapter;
            _retornoChatAdapter = retornoChatAdapter;
            _variacaoDeTreinoAdapter = variacaoDeTreinoAdapter;
            _treinoBuilder = treinoBuilder;
        }

        public async Task<Treino?> MontarTreino(Pessoa pessoa)
        {
            var prompt = _promptAdapter.ConstruirPrompt(pessoa);
            var treinoProposto = await _retornoChatAdapter.ExtrairRespostaDoChat(prompt);

            var variacaoDeTreino = _variacaoDeTreinoAdapter.ListarVariacaoDeTreinos(treinoProposto);
            var quantidadeVariacoes = variacaoDeTreino.Count;

            var estimativaDeDuracaoEmDias = 120;

            variacaoDeTreino.ForEach(variacao =>
            {
                _treinoBuilder.ComVariacao(variacao);
            });

            return _treinoBuilder
                .ComDataInicio(DateTime.Now)
                .ComDataFim(DateTime.Now.AddDays(estimativaDeDuracaoEmDias))
                .Build();
        }
    }
}
