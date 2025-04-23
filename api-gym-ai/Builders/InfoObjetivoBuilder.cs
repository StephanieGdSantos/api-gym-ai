using api_gym_ai.Interfaces;
using api_gym_ai.Models;

namespace api_gym_ai.Builders
{
    public class InfoObjetivoBuilder : IInfoObjetivoBuilder
    {
        private InfoObjetivo _infoObjetivo = new();

        public IInfoObjetivoBuilder ComObjetivo(string objetivo)
        {
            _infoObjetivo.Objetivo = objetivo;
            return this;
        }

        public IInfoObjetivoBuilder ComPartesDoCorpoEmFoco(string partesDoCorpo)
        {
            _infoObjetivo.PartesDoCorpoEmFoco = partesDoCorpo.Split(',').Select(p => p.Trim());
            return this;
        }

        public IInfoObjetivoBuilder ComTempoDeTreino(double tempo)
        {
            _infoObjetivo.TempoDeTreino = tempo;
            return this;
        }

        public InfoObjetivo Build()
        {
            return _infoObjetivo;
        }
    }
}
