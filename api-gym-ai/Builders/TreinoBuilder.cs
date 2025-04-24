using api_gym_ai.Models;

namespace api_gym_ai.Builders
{
    public class TreinoBuilder
    {
        private readonly Treino _treino = new();
        public TreinoBuilder ComVariacaoA(VariacaoDeTreino variacaoDeTreino)
        {
            _treino.VariacaoDeTreino.Add(variacaoDeTreino);
            return this;
        }
        public TreinoBuilder ComVariacaoB(VariacaoDeTreino variacaoDeTreino)
        {
            _treino.VariacaoDeTreino.Add(variacaoDeTreino);
            return this;
        }
        public TreinoBuilder ComVariacaoC(VariacaoDeTreino variacaoDeTreino)
        {
            _treino.VariacaoDeTreino.Add(variacaoDeTreino);
            return this;
        }
        public TreinoBuilder ComVariacaoD(VariacaoDeTreino variacaoDeTreino)
        {
            _treino.VariacaoDeTreino.Add(variacaoDeTreino);
            return this;
        }
        public TreinoBuilder ComDataInicio(DateTime dataInicio)
        {
            _treino.DataInicio = dataInicio;
            return this;
        }
        public TreinoBuilder ComDataFim(DateTime dataFim)
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
