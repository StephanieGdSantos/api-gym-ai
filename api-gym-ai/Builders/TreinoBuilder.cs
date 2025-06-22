using api_gym_ai.Interfaces.Builders;
using api_gym_ai.Models;

namespace api_gym_ai.Builders
{
    public class TreinoBuilder : ITreinoBuilder
    {
        private readonly Treino _treino = new();

        public TreinoBuilder()
        {
            _treino.VariacaoDeTreino = new List<VariacaoDeTreino>();
        }

        public ITreinoBuilder ComVariacao(VariacaoDeTreino variacaoDeTreino)
        {
            _treino.VariacaoDeTreino.Add(variacaoDeTreino);
            return this;
        }
        public ITreinoBuilder ComDataInicio(string dataInicio)
        {
            _treino.DataInicio = dataInicio;
            return this;
        }
        public ITreinoBuilder ComDataFim(string dataFim)
        {
            _treino.DataFim = dataFim;
            return this;
        }
        public Treino Build()
        {
            return _treino;
        }
    }
}
