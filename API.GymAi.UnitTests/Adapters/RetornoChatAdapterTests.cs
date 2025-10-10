using APIGymAi.Adapters;
using APIGymAi.Adapters.Interfaces;
using APIGymAi.Facades;
using APIGymAi.Interfaces.Builders;
using APIGymAi.Models;
using APIGymAi.Services.Interface;
using Moq;
using System.Text.Json;

namespace APIGymAi.UnitTests.Adapters;

public class RetornoChatAdapterTests
{
    private readonly Mock<IChatService> _cohereService;
    private readonly IRetornoChatAdapter _retornoChatAdapter;

    public RetornoChatAdapterTests()
    {
        _cohereService = new Mock<IChatService>();
        _retornoChatAdapter = new RetornoChatAdapter(_cohereService.Object);
    }

    [Fact]
    public void ExtrairRespostaDoChat_DeveRetornarMensagemDoChat_QuandoRetornoForValido()
    {
        // Arrange
        var prompt = new Prompt { Mensagem = "Qual é o treino ideal?" };

        var content = new Content
        {
            type = "text",
            text = "O treino ideal é aquele que atende às suas necessidades."
        };

        var retornoChat = new RetornoChat
        {
            id = "abc",
            message = new Message
            {
                role = "assistant",
                content = new[]
                {
                    content
                }
            }
        };

        _cohereService.Setup(s => s.ChatAsync(prompt.Mensagem))
            .ReturnsAsync(JsonSerializer.Serialize(retornoChat));

        // Act
        var resultado = _retornoChatAdapter.ExtrairRespostaDoChat(prompt).Result;

        // Assert
        Assert.Equal("O treino ideal é aquele que atende às suas necessidades.", resultado);
    }

    [Fact]
    public void ExtrairRespostaDoChat_DeveLancarExcecao_QuandoRetornoForInvalido()
    {
        // Arrange
        var prompt = new Prompt { Mensagem = "Qual é o treino ideal?" };
        _cohereService.Setup(s => s.ChatAsync(prompt.Mensagem))
            .ReturnsAsync("{}");

        // Act & Assert
        Assert.ThrowsAsync<Exception>(() => _retornoChatAdapter.ExtrairRespostaDoChat(prompt));
    }

    [Fact]
    public void FormatarRetornoDoChat_DeveRemoverQuebrasDeLinhaETravarEspacos()
    {
        // Arrange
        var mensagemChat = "Esta é uma mensagem\n com quebras de linha\r\n e espaços extras.   ";

        // Act
        var resultado = _retornoChatAdapter.FormatarRetornoDoChat(mensagemChat);

        // Assert
        Assert.Equal("Esta é uma mensagem  com quebras de linha   e espaços extras.", resultado);
    }
}