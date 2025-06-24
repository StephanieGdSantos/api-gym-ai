using api_gym_ai.Facades;
using api_gym_ai.Interfaces.Builders;
using api_gym_ai.Interfaces.Services;
using api_gym_ai.Models;
using Moq;
using System.Text.Json;
using static api_gym_ai.Models.InfoPreferencias;

namespace api_gym_ai.Test;

public class PromptAdapterTestes
{
    private readonly Mock<IPromptBuilder> _mockPromptBuilder;
    private readonly PromptAdapter _adaptadorPrompt;

    public PromptAdapterTestes()
    {
        _mockPromptBuilder = new Mock<IPromptBuilder>();
        _adaptadorPrompt = new PromptAdapter(_mockPromptBuilder.Object);
    }

    [Fact]
    public void ConstruirPrompt_DeveRetornarPrompt_QuandoPessoaForValida()
    {
        // Arrange
        var enumObjetivo = It.IsAny<EnumObjetivo>();
        var enumPartesDoCorpoEmFoco = new List<EnumPartesDoCorpoEmFoco>
        {
            It.IsAny<EnumPartesDoCorpoEmFoco>()
        };

        var pessoa = new Pessoa
        {
            Idade = 30,
            Peso = 80,
            Altura = 180,
            InfoCorporais = new InfoCorporais
            {
                MassaMuscular = 40,
                PercentualGordura = 20,
                Limitacoes = new[] { "Lesão no joelho" }
            },
            InfoPreferencias = new InfoPreferencias
            {
                PartesDoCorpoEmFoco = enumPartesDoCorpoEmFoco,
                Objetivo = enumObjetivo,
                TempoDeTreinoEmMinutos = 45,
                VariacaoTreino = "Alta",
                Observacao = "Baixa"
            }
        };

        var promptEsperado = new Prompt();
        _mockPromptBuilder.Setup(b => b.ComIdade("30")).Returns(_mockPromptBuilder.Object);
        _mockPromptBuilder.Setup(b => b.ComPeso("80")).Returns(_mockPromptBuilder.Object);
        _mockPromptBuilder.Setup(b => b.ComAltura("180")).Returns(_mockPromptBuilder.Object);
        _mockPromptBuilder.Setup(b => b.ComMassaMuscular("40")).Returns(_mockPromptBuilder.Object);
        _mockPromptBuilder.Setup(b => b.ComPercentualDeGordura("20")).Returns(_mockPromptBuilder.Object);
        _mockPromptBuilder.Setup(b => b.ComLimitacoes("Lesão no joelho")).Returns(_mockPromptBuilder.Object);
        _mockPromptBuilder.Setup(b => b.ComPartesDoCorpoEmFoco("Braços, Pernas")).Returns(_mockPromptBuilder.Object);
        _mockPromptBuilder.Setup(b => b.ComObjetivo("Perder peso")).Returns(_mockPromptBuilder.Object);
        _mockPromptBuilder.Setup(b => b.ComTempoDeTreinoEmMinutos("45")).Returns(_mockPromptBuilder.Object);
        _mockPromptBuilder.Setup(b => b.ComVariacaoDeTreino("Alta")).Returns(_mockPromptBuilder.Object);
        _mockPromptBuilder.Setup(b => b.ComObservacao("Baixa")).Returns(_mockPromptBuilder.Object);
        _mockPromptBuilder.Setup(b => b.Build()).Returns(promptEsperado);

        // Act
        var resultado = _adaptadorPrompt.ConstruirPrompt(pessoa);

        // Assert
        Assert.Equal(promptEsperado, resultado);
    }

    [Fact]
    public void ConstruirPrompt_DeveLancarExcecao_QuandoPessoaForNula()
    {
        // Arrange
        var pessoa = (Pessoa)null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _adaptadorPrompt.ConstruirPrompt(pessoa));
    }
}