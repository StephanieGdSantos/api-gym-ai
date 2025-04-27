using api_gym_ai.Models;

namespace api_gym_ai.Interfaces.Builders
{
    public interface IPessoaBuilder
    {
        public IPessoaBuilder ComAltura(double altura);
        public IPessoaBuilder ComIdade(int idade);
        public IPessoaBuilder ComPeso(double peso);
        public Pessoa Build();
    }
}
