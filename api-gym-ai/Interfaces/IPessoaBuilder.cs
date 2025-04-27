using api_gym_ai.Models;

namespace api_gym_ai.Interfaces
{
    public interface IPessoaBuilder
    {
        public IPessoaBuilder ComAltura(double altura);
        public IPessoaBuilder ComIdade(int idade);
        public IPessoaBuilder ComPeso(double peso);
        public Pessoa Build();
    }
}
