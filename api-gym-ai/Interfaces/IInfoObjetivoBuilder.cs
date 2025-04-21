using api_gym_ai.Models;

namespace api_gym_ai.Interfaces
{
    public interface IInfoObjetivoBuilder
    {
        public IInfoObjetivoBuilder ComObjetivo(string objetivo);
        public IInfoObjetivoBuilder ComTempoDeTreino(string tempo);
        public IInfoObjetivoBuilder ComPartesDoCorpoEmFoco(string partesDoCorpo);

        public InfoObjetivo Build();
    }
}
