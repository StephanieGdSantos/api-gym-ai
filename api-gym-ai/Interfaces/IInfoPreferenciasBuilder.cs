using api_gym_ai.Models;

namespace api_gym_ai.Interfaces
{
    public interface IInfoPreferenciasBuilder
    {
        public IInfoPreferenciasBuilder ComObjetivo(string objetivo);
        public IInfoPreferenciasBuilder ComTempoDeTreino(double tempo);
        public IInfoPreferenciasBuilder ComPartesDoCorpoEmFoco(string partesDoCorpo);

        public InfoPreferencias Build();
    }
}
