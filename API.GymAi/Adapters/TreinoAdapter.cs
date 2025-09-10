using API.GymAi.Adapters;
using API.GymAi.Adapters.Interfaces;
using API.GymAi.Builders.Interfaces;
using API.GymAi.Models;
using API.GymAi.Options;
using API.GymAi.Services;
using API.GymAi.Utils;
using Microsoft.Extensions.Options;
using System.Globalization;
using static API.GymAi.Models.InfoPreferencias;

namespace API.GymAi.Facades
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
            try
            {
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
            catch (Exception ex)
            {
                throw new Exception("Erro ao montar o treino.", ex);
            }
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
