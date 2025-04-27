using api_gym_ai.Models;

namespace api_gym_ai.Interfaces.Builders
{
    public interface ITreinoBuilder
    {
        public ITreinoBuilder ComVariacaoA(VariacaoDeTreino variacaoDeTreino);
        public ITreinoBuilder ComVariacaoB(VariacaoDeTreino variacaoDeTreino);
        public ITreinoBuilder ComVariacaoC(VariacaoDeTreino variacaoDeTreino);
        public ITreinoBuilder ComVariacaoD(VariacaoDeTreino variacaoDeTreino);
        public ITreinoBuilder ComDataInicio(DateTime dataInicio);
        public ITreinoBuilder ComDataFim(DateTime dataFim);
        public Treino Build();
    }
}
