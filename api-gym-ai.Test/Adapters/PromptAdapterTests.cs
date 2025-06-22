using api_gym_ai.Facades;
using api_gym_ai.Interfaces.Builders;
using api_gym_ai.Interfaces.Services;
using api_gym_ai.Models;
using Moq;
using System.Text.Json;

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
                PartesDoCorpoEmFoco = new[] { "Braços", "Pernas" },
                Objetivo = "Perder peso",
                TempoDeTreino = 45,
                VariacaoTreino = "Alta",
                VariacaoMuscular = "Baixa"
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
        _mockPromptBuilder.Setup(b => b.ComTempoDeTreino("45")).Returns(_mockPromptBuilder.Object);
        _mockPromptBuilder.Setup(b => b.ComVariacaoDeTreino("Alta")).Returns(_mockPromptBuilder.Object);
        _mockPromptBuilder.Setup(b => b.ComVariacaoMuscular("Baixa")).Returns(_mockPromptBuilder.Object);
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