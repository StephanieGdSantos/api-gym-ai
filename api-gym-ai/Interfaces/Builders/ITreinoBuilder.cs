using api_gym_ai.Models;

namespace api_gym_ai.Interfaces.Builders
{
    public interface ITreinoBuilder
    {
        public ITreinoBuilder ComVariacao(VariacaoDeTreino variacaoDeTreino);
        public ITreinoBuilder ComDataInicio(string dataInicio);
        public ITreinoBuilder ComDataFim(string dataFim);
        public Treino Build();
    }
}
