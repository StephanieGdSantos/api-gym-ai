using Microsoft.Extensions.Options;

namespace api_gym_ai.Options
{
    public class InformacoesPromptOptions
    {
        public int QuantidadeMinimaExercicios { get; set; }
        public int QuantidadeMaximaExercicios { get; set; }
        public string BasePrompt {  get; set; }
    }
}
