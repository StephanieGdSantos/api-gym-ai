using api_gym_ai.Models;

namespace api_gym_ai.Interfaces.Builders
{
    public interface IInfoCorporaisBuilder
    {
        public IInfoCorporaisBuilder ComLimitacoes(IEnumerable<string> limitacoes);
        public IInfoCorporaisBuilder ComMassaMuscular(double? massaMuscular);
        public IInfoCorporaisBuilder ComPercentualGordura(double? percentualGordura);
        public IInfoCorporaisBuilder ComSexo(string? sexo);
        public InfoCorporais Build();
    }
}
