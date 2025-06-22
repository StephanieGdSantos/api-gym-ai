using api_gym_ai.Builders;
using api_gym_ai.Interfaces.Adapters;
using api_gym_ai.Interfaces.Builders;
using api_gym_ai.Models;
using System.Globalization;

namespace api_gym_ai.Facades
{
    public class TreinoAdapter : ITreinoAdapter
    {
        private readonly IPromptAdapter _promptAdapter;
        private readonly IRetornoChatAdapter _retornoChatAdapter;
        private readonly ITreinoBuilder _treinoBuilder;

        public TreinoAdapter(IPromptAdapter promptAdapter, IRetornoChatAdapter retornoChatAdapter, ITreinoBuilder treinoBuilder)
        {
            _promptAdapter = promptAdapter;
            _retornoChatAdapter = retornoChatAdapter;
            _treinoBuilder = treinoBuilder;
        }

        public async Task<Treino?> MontarTreino(Pessoa pessoa)
        {
            var prompt = _promptAdapter.ConstruirPrompt(pessoa);

            var retornoChat = await _retornoChatAdapter.ExtrairRespostaDoChat(prompt);

            var treinoProposto = JsonSerializer.Deserialize<Treino>(retornoChat);

            treinoProposto.VariacaoDeTreino.ForEach(variacao =>
            {
                _treinoBuilder.ComVariacao(variacao);
            });

            var formatoDeData = CultureInfo.GetCultureInfo("pt-BR").DateTimeFormat;
            var estimativaDeDuracaoEmDias = 120;
            var dataInicio = DateTime.Now.Date;
            var dataFim = dataInicio.AddDays(estimativaDeDuracaoEmDias);

            return _treinoBuilder
                .ComDataInicio(dataInicio.ToString(formatoDeData))
                .ComDataFim(dataFim.ToString(formatoDeData))
                .Build();
        }
    }
}
