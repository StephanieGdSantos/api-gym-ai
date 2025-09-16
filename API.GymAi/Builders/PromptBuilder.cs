using API.GymAi.Builders.Interfaces;
using API.GymAi.Models;
using API.GymAi.Options;
using Microsoft.Extensions.Options;
using System.Numerics;
using static API.GymAi.Models.InfoPreferencias;

namespace API.GymAi.Builders
{
    /// <summary>
    /// Classe responsável por construir prompts personalizados com base nas informações fornecidas.
    /// </summary>
    public class PromptBuilder : IPromptBuilder
    {
        private readonly IOptions<InformacoesPromptOptions> _informacoesPromptOptions;

        private readonly int _quantidadeMinimaDeExercicios;
        private readonly int _quantidadeMaximaDeExercicios;
        private readonly string _basePrompt;

        private string _informacoes = string.Empty;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="PromptBuilder"/>.
        /// </summary>
        /// <param name="informacoesPromptOptions">Opções de configuração para o prompt.</param>
        public PromptBuilder(IOptions<InformacoesPromptOptions> informacoesPromptOptions)
        {
            _informacoesPromptOptions = informacoesPromptOptions ?? throw new ArgumentNullException(nameof(informacoesPromptOptions));
            _basePrompt = _informacoesPromptOptions.Value.BasePrompt;
            _quantidadeMaximaDeExercicios = _informacoesPromptOptions.Value.QuantidadeMaximaExercicios;
            _quantidadeMinimaDeExercicios = _informacoesPromptOptions.Value.QuantidadeMinimaExercicios;
        }

        /// <summary>  
        /// Adiciona a altura às informações do prompt.  
        /// </summary>  
        /// <param name="altura">A altura da pessoa.</param>  
        /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento de chamadas.</returns>  
        public IPromptBuilder ComAltura(string altura)
        {
            _informacoes += $"Altura: {altura}, ";

            return this;
        }

        /// <summary>  
        /// Adiciona a idade às informações do prompt.  
        /// </summary>  
        /// <param name="idade">A idade da pessoa.</param>  
        /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento de chamadas.</returns>  
        public IPromptBuilder ComIdade(string idade)
        {
            _informacoes += $"Idade: {idade}, ";

            return this;
        }

        /// <summary>  
        /// Adiciona as limitações às informações do prompt.  
        /// </summary>  
        /// <param name="limitacoes">As limitações da pessoa.</param>  
        /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento de chamadas.</returns>  
        public IPromptBuilder ComLimitacoes(string? limitacoes)
        {
            if (!string.IsNullOrEmpty(limitacoes))
                _informacoes += $"Limitações: {limitacoes}, ";

            return this;
        }

        /// <summary>  
        /// Adiciona a massa muscular às informações do prompt.  
        /// </summary>  
        /// <param name="massaMuscular">A massa muscular da pessoa.</param>  
        /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento de chamadas.</returns>  
        public IPromptBuilder ComMassaMuscular(string? massaMuscular)
        {
            if (!string.IsNullOrEmpty(massaMuscular))
                _informacoes += $"Massa Muscular: {massaMuscular}, ";

            return this;
        }

        /// <summary>  
        /// Adiciona o objetivo às informações do prompt.  
        /// </summary>  
        /// <param name="objetivo">O objetivo da pessoa.</param>  
        /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento de chamadas.</returns>  
        public IPromptBuilder ComObjetivo(string objetivo)
        {
            _informacoes += $"Objetivo: {objetivo}, ";
            return this;
        }

        /// <summary>  
        /// Adiciona as partes do corpo em foco às informações do prompt.  
        /// </summary>  
        /// <param name="partesDoCorpoEmFoco">As partes do corpo que serão o foco do treino.</param>  
        /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento de chamadas.</returns>  
        public IPromptBuilder ComPartesDoCorpoEmFoco(string partesDoCorpoEmFoco)
        {
            _informacoes += $"Partes do Corpo em Foco: {partesDoCorpoEmFoco}, ";

            return this;
        }

        /// <summary>  
        /// Adiciona o percentual de gordura às informações do prompt.  
        /// </summary>  
        /// <param name="percentualGordura">O percentual de gordura da pessoa.</param>  
        /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento de chamadas.</returns>  
        public IPromptBuilder ComPercentualDeGordura(string? percentualGordura)
        {
            if (!string.IsNullOrEmpty(percentualGordura))
                _informacoes += $"Percentual de Gordura: {percentualGordura}, ";

            return this;
        }

        /// <summary>  
        /// Adiciona o peso às informações do prompt.  
        /// </summary>  
        /// <param name="peso">O peso da pessoa.</param>  
        /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento de chamadas.</returns>  
        public IPromptBuilder ComPeso(string peso)
        {
            _informacoes += $"Peso: {peso}, ";

            return this;
        }

        /// <summary>  
        /// Adiciona o sexo às informações do prompt.  
        /// </summary>  
        /// <param name="sexo">O sexo da pessoa.</param>  
        /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento de chamadas.</returns>  
        public IPromptBuilder ComSexo(string? sexo)
        {
            if (!string.IsNullOrEmpty(sexo))
                _informacoes += $"Sexo: {sexo}, ";

            return this;
        }

        /// <summary>  
        /// Adiciona o tempo de treino em minutos às informações do prompt.  
        /// </summary>  
        /// <param name="tempoDeTreino">O tempo de treino em minutos para cada variação.</param>  
        /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento de chamadas.</returns>  
        public IPromptBuilder ComTempoDeTreinoEmMinutos(string tempoDeTreino)
        {
            _informacoes += $"Tempo de Treino: {tempoDeTreino} minutos para cada variação, ";

            return this;
        }

        /// <summary>  
        /// Adiciona a variação de treino às informações do prompt.  
        /// </summary>  
        /// <param name="variacao">A variação de treino a ser adicionada.</param>  
        /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento de chamadas.</returns>  
        public IPromptBuilder ComVariacaoDeTreino(string variacao)
        {
            _informacoes += $"Variação de Treino: {variacao}, ";

            return this;
        }

        /// <summary>  
        /// Adiciona uma observação sobre a variação muscular às informações do prompt.  
        /// </summary>  
        /// <param name="variacaoMuscular">A variação muscular a ser adicionada.</param>  
        /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento de chamadas.</returns>  
        public IPromptBuilder ComObservacao(string variacaoMuscular)
        {
            _informacoes += $"Variação Muscular: {variacaoMuscular}, ";

            return this;
        }

        /// <summary>  
        /// Adiciona o nível às informações do prompt.  
        /// </summary>  
        /// <param name="nivel">O nível de experiência da pessoa.</param>  
        /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento de chamadas.</returns>  
        public IPromptBuilder ComNivel(string nivel)
        {
            _informacoes += $"Nível: {nivel}, ";
            return this;
        }

        /// <summary>  
        /// Constrói um objeto <see cref="Prompt"/> com base nas informações fornecidas.  
        /// </summary>  
        /// <returns>Um objeto <see cref="Prompt"/> contendo o prompt formatado.</returns>  
        public Prompt Build()
        {
            _informacoes = _informacoes.TrimEnd(',', ' ');

            FormatarBasePrompt();

            Prompt promptFinal = new()
            {
                Mensagem = _basePrompt
                .Replace("[informações]", _informacoes)
            };

            return promptFinal;
        }

        /// <summary>  
        /// Formata o prompt base substituindo os placeholders pelas informações configuradas.  
        /// </summary>  
        /// <returns>Uma string contendo o prompt base formatado.</returns>  
        public string FormatarBasePrompt()
        {
            return _basePrompt
                .Replace("[quantidadeMinimaDeExercicios]", _quantidadeMinimaDeExercicios.ToString())
                .Replace("[quantidadeMaximaDeExercicios]", _quantidadeMaximaDeExercicios.ToString());
        }
    }
}
