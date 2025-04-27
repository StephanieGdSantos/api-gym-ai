using api_gym_ai.Interfaces.Builders;
using api_gym_ai.Models;

namespace api_gym_ai.Builders
{
    public class InfoPreferenciasBuilder : IInfoPreferenciasBuilder
    {
        private InfoPreferencias _infoObjetivo = new();

        public IInfoPreferenciasBuilder ComObjetivo(string objetivo)
        {
            _infoObjetivo.Objetivo = objetivo;
            return this;
        }

        public IInfoPreferenciasBuilder ComPartesDoCorpoEmFoco(string partesDoCorpo)
        {
            _infoObjetivo.PartesDoCorpoEmFoco = partesDoCorpo.Split(',').Select(p => p.Trim());
            return this;
        }

        public IInfoPreferenciasBuilder ComTempoDeTreino(double tempo)
        {
            _infoObjetivo.TempoDeTreino = tempo;
            return this;
        }

        public InfoPreferencias Build()
        {
            return _infoObjetivo;
        }
    }
}
