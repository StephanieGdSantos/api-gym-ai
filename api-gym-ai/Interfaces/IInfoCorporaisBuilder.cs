using api_gym_ai.Models;

namespace api_gym_ai.Interfaces
{
    public interface IInfoCorporaisBuilder
    {
        public IInfoCorporaisBuilder ComAltura(double altura);
        public IInfoCorporaisBuilder ComIdade(int idade);
        public IInfoCorporaisBuilder ComLimitacoes(IEnumerable<string> limitacoes);
        public IInfoCorporaisBuilder ComMassaMuscular(double? massaMuscular);
        public IInfoCorporaisBuilder ComPercentualGordura(double? percentualGordura);
        public IInfoCorporaisBuilder ComPeso(double peso);
        public IInfoCorporaisBuilder ComSexo(string? sexo);
        public InfoCorporais Build();
    }
}
