using api_gym_ai.Interfaces.Builders;
using api_gym_ai.Models;

namespace api_gym_ai.Builders
{
    public class PessoaBuilder : IPessoaBuilder
    {
        private Pessoa _pessoa = new();

        public IPessoaBuilder ComAltura(double altura)
        {
            _pessoa.Altura = altura;
            return this;
        }

        public IPessoaBuilder ComIdade(int idade)
        {
            _pessoa.Idade = idade;
            return this;
        }

        public IPessoaBuilder ComPeso(double peso)
        {
            _pessoa.Peso = peso;
            return this;
        }

        public Pessoa Build()
        {
            return _pessoa;
        }
    }
}
