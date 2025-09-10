using API.GymAi.Builders.Interfaces;
using API.GymAi.Models;

namespace API.GymAi.Builders
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

        public ITreinoBuilder ComPeriodo(PeriodoTreino periodo)
        {
            _treino.Periodo = periodo;
            return this;
        }
        public Treino Build()
        {
            return _treino;
        }
    }
}
