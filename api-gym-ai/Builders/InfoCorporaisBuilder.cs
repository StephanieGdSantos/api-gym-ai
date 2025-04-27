using api_gym_ai.Interfaces;
using api_gym_ai.Models;

namespace api_gym_ai.Builders
{
    public class InfoCorporaisBuilder : IInfoCorporaisBuilder
    {
        private InfoCorporais _infoCorporais = new();

        public IInfoCorporaisBuilder ComLimitacoes(IEnumerable<string> limitacoes)
        {
            _infoCorporais.Limitacoes = limitacoes;
            return this;
        }

        public IInfoCorporaisBuilder ComMassaMuscular(double? massaMuscular)
        {
            if (massaMuscular.HasValue)
                _infoCorporais.MassaMuscular = massaMuscular;

            return this;
        }

        public IInfoCorporaisBuilder ComPercentualGordura(double? percentualGordura)
        {
            if (percentualGordura.HasValue)
                _infoCorporais.PercentualGordura = percentualGordura;

            return this;
        }

        public IInfoCorporaisBuilder ComSexo(string? sexo)
        {
            if (!string.IsNullOrEmpty(sexo))
                _infoCorporais.Sexo = sexo;

            return this;
        }

        public InfoCorporais Build()
        {
            return _infoCorporais;
        }
    }
}
