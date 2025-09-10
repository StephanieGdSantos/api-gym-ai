using API.GymAi.Models;

namespace API.GymAi.Builders.Interfaces
{
    public interface ITreinoBuilder
    {
        public ITreinoBuilder ComVariacao(VariacaoDeTreino variacaoDeTreino);
        public ITreinoBuilder ComPeriodo(PeriodoTreino periodo);
        public Treino Build();
    }
}
