using api_gym_ai.Adapters;
using api_gym_ai.Interfaces.Adapters;
using api_gym_ai.Interfaces.Builders;
using api_gym_ai.Models;
using api_gym_ai.Options;
using api_gym_ai.Services;
using api_gym_ai.Utils;
using Microsoft.Extensions.Options;
using System.Globalization;
using static api_gym_ai.Models.InfoPreferencias;

namespace api_gym_ai.Facades
{
    public class TreinoAdapter : ITreinoAdapter
    {
        private readonly IPromptAdapter _promptAdapter;
        private readonly IRetornoChatAdapter _retornoChatAdapter;
        private readonly ITreinoBuilder _treinoBuilder;
        private readonly PeriodoDeTreinoOptions _periodoDeTreinoOptions;

        public TreinoAdapter(IPromptAdapter promptAdapter, IRetornoChatAdapter retornoChatAdapter, ITreinoBuilder treinoBuilder,
            IOptions<PeriodoDeTreinoOptions> periodoDeTreinoOptions)
        {
            _promptAdapter = promptAdapter;
            _retornoChatAdapter = retornoChatAdapter;
            _treinoBuilder = treinoBuilder;
            _periodoDeTreinoOptions = periodoDeTreinoOptions.Value;
        }

        public async Task<Treino?> MontarTreino(Pessoa pessoa)
        {
            if (pessoa == null)
                throw new ArgumentNullException(nameof(pessoa), "A pessoa não pode ser nula.");

            var prompt = _promptAdapter.ConstruirPrompt(pessoa);

            var retornoChat = await _retornoChatAdapter.ExtrairRespostaDoChat(prompt);

            var treinoProposto = JsonUtils.DeserializeOrThrow<Treino>(retornoChat, nameof(Treino));

            treinoProposto.VariacaoDeTreino.ForEach(variacao =>
            {
                _treinoBuilder.ComVariacao(variacao);
            });

            var periodo = CalcularPeriodoDeTreino(pessoa.InfoPreferencias.Nivel);

            return _treinoBuilder
                .ComPeriodo(periodo)
                .Build();
        }

        public PeriodoTreino CalcularPeriodoDeTreino(EnumNivelCondicionamento nivelCondicionamento)
        {
            var formatoDeData = CultureInfo.GetCultureInfo("pt-BR").DateTimeFormat;
            var estimativaDeDuracaoEmDias = ConsultarPeriodoPorCondicionamento(nivelCondicionamento);
            var dataInicio = DateTime.Now.Date;
            var dataFim = dataInicio.AddDays(estimativaDeDuracaoEmDias);

            return new PeriodoTreino
            {
                DataInicio = dataInicio.ToString(formatoDeData),
                DataFim = dataFim.ToString(formatoDeData)
            };
        }

        public int ConsultarPeriodoPorCondicionamento(EnumNivelCondicionamento nivelCondicionamento)
        {
            return nivelCondicionamento switch
            {
                EnumNivelCondicionamento.Iniciante => _periodoDeTreinoOptions.Iniciante,
                EnumNivelCondicionamento.Intermediario => _periodoDeTreinoOptions.Intermediario,
                EnumNivelCondicionamento.Avancado => _periodoDeTreinoOptions.Avancado,
                _ => throw new ArgumentOutOfRangeException(nameof(nivelCondicionamento), "Nível de condicionamento inválido.")
            };
        }
    }
}
