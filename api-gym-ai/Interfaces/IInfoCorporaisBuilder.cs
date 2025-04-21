using api_gym_ai.Models;

namespace api_gym_ai.Interfaces
{
    public interface IInfoCorporaisBuilder
    {
        public IInfoCorporaisBuilder ComAltura(string altura);
        public IInfoCorporaisBuilder ComIdade(string idade);
        public IInfoCorporaisBuilder ComLimitacoes(IEnumerable<string> limitacoes);
        public IInfoCorporaisBuilder ComMassaMuscular(string? massaMuscular);
        public IInfoCorporaisBuilder ComPercentualGordura(string? percentualGordura);
        public IInfoCorporaisBuilder ComPeso(string peso);
        public IInfoCorporaisBuilder ComSexo(string? sexo);
        public InfoCorporais Build();
    }
}
