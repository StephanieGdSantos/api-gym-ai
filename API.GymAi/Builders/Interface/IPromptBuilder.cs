using API.GymAi.Models;
using static API.GymAi.Models.InfoPreferencias;

namespace API.GymAi.Builders.Interfaces
{
    public interface IPromptBuilder
    {
        /// <summary>
        /// Monta o prompt com as informações fornecidas
        /// </summary>

        public IPromptBuilder ComObjetivo(string objetivo);
        public IPromptBuilder ComTempoDeTreinoEmMinutos(string tempoDeTreino);
        public IPromptBuilder ComPartesDoCorpoEmFoco(string partesDoCorpoEmFoco);
        public IPromptBuilder ComLimitacoes(string? limitacoes);
        public IPromptBuilder ComSexo(string? sexo);
        public IPromptBuilder ComIdade(string idade);
        public IPromptBuilder ComAltura(string altura);
        public IPromptBuilder ComPeso(string peso);
        public IPromptBuilder ComMassaMuscular(string? massaMuscular);
        public IPromptBuilder ComPercentualDeGordura(string? percentualGordura);
        public IPromptBuilder ComVariacaoDeTreino(string variacao);
        public IPromptBuilder ComObservacao(string variacaoMuscular);
        public IPromptBuilder ComNivel(string nivel);
        public Prompt Build();
    }
}
