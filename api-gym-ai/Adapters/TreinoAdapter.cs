using api_gym_ai.Builders;
using api_gym_ai.Interfaces;
using api_gym_ai.Models;

namespace api_gym_ai.Facades
{
    public class TreinoAdapter : ITreinoAdapter
    {
        private readonly IPromptAdapter _promptAdapter;
        private readonly IVariacaoDeTreinoAdapter _variacaoDeTreinoAdapter;
        private readonly IExercicioAdapter _exercicioAdapter;
        private readonly IExercicioBuilder _exercicioBuilder;

        public TreinoAdapter(IPromptAdapter promptAdapter, IVariacaoDeTreinoAdapter variacaoDeTreinoAdapter, IExercicioAdapter exercicioAdapter, IExercicioBuilder exercicioBuilder)
        {
            _promptAdapter = promptAdapter;
            _variacaoDeTreinoAdapter = variacaoDeTreinoAdapter;
            _exercicioAdapter = exercicioAdapter;
            _exercicioBuilder = exercicioBuilder;
        }

        public Treino? MontarTreino(Pessoa pessoa)
        {
            var promptFinal = _promptAdapter.ConstruirPrompt(pessoa);
            var treinoProposto = _promptAdapter.ExtrairRespostaDoChat(promptFinal);
            var variacaoDeTreino = _variacaoDeTreinoAdapter.ListarVariacaoDeTreinos(treinoProposto);

            var quantidadeVariacoes = variacaoDeTreino.Count;

            switch (quantidadeVariacoes)
            {
                case 2:
                    return new TreinoBuilder()
                        .ComVariacaoA(variacaoDeTreino[0])
                        .ComVariacaoB(variacaoDeTreino[1])
                        .ComDataInicio(DateTime.Now)
                        .ComDataFim(DateTime.Now.AddDays(120))
                        .Build();
                case 3:
                    return new TreinoBuilder()
                        .ComVariacaoA(variacaoDeTreino[0])
                        .ComVariacaoB(variacaoDeTreino[1])
                        .ComVariacaoC(variacaoDeTreino[2])
                        .ComDataInicio(DateTime.Now)
                        .ComDataFim(DateTime.Now.AddDays(120))
                        .Build();
                case 4:
                    return new TreinoBuilder()
                        .ComVariacaoA(variacaoDeTreino[0])
                        .ComVariacaoB(variacaoDeTreino[1])
                        .ComVariacaoC(variacaoDeTreino[2])
                        .ComVariacaoD(variacaoDeTreino[3])
                        .ComDataInicio(DateTime.Now)
                        .ComDataFim(DateTime.Now.AddDays(120))
                        .Build();
            }

            return null;
        }
    }
}
