using api_gym_ai.Interfaces;
using api_gym_ai.Models;

namespace api_gym_ai.Builders
{
    public class InfoCorporaisBuilder : IInfoCorporaisBuilder
    {
        private InfoCorporais _infoCorporais = new();

        public IInfoCorporaisBuilder ComAltura(string altura)
        {
            _infoCorporais.Altura = altura;
            return this;
        }

        public IInfoCorporaisBuilder ComIdade(string idade)
        {
            _infoCorporais.Idade = idade;
            return this;
        }

        public IInfoCorporaisBuilder ComLimitacoes(IEnumerable<string> limitacoes)
        {
            _infoCorporais.Limitacoes = limitacoes;
            return this;
        }

        public IInfoCorporaisBuilder ComMassaMuscular(string? massaMuscular)
        {
            if (!string.IsNullOrEmpty(massaMuscular))
                _infoCorporais.MassaMuscular = massaMuscular;

            return this;
        }

        public IInfoCorporaisBuilder ComPercentualGordura(string? percentualGordura)
        {
            if (!string.IsNullOrEmpty(percentualGordura))
                _infoCorporais.PercentualGordura = percentualGordura;

            return this;
        }

        public IInfoCorporaisBuilder ComPeso(string peso)
        {
            _infoCorporais.Peso = peso;
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
