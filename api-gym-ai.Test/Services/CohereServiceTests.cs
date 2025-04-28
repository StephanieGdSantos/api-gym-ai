using System.Net;
using api_gym_ai.Services;
using Moq;
using Moq.Protected;

namespace api_gym_ai.Tests;

public class CohereServiceTests
{
    [Fact]
    public async Task ChatAsync_DeveRetornarRespostaComSucesso()
    {
        // Arrange
        var mensagem = "Teste de prompt";
        var respostaEsperada = "{\"message\":{\"content\":[{\"text\":\"Resposta da API\"}]}}";

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(respostaEsperada)
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var cohereService = new CohereService(httpClient);

        // Act
        var resposta = await cohereService.ChatAsync(mensagem);

        // Assert
        Assert.Equal(respostaEsperada, resposta);
    }

    [Fact]
    public async Task ChatAsync_DeveLancarExcecaoQuandoApiRetornaErro()
    {
        // Arrange
        var mensagem = "Teste de erro";

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent("Erro da API")
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var cohereService = new CohereService(httpClient);

        // Act & Assert
        var excecao = await Assert.ThrowsAsync<ApplicationException>(() => cohereService.ChatAsync(mensagem));
        Assert.Contains("Erro da API Cohere", excecao.Message);
    }

    [Fact]
    public async Task ChatAsync_DeveLancarExcecaoQuandoConexaoFalha()
    {
        // Arrange
        var mensagem = "Teste de falha de conexão";

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Falha de conexão"));

        var httpClient = new HttpClient(handlerMock.Object);
        var cohereService = new CohereService(httpClient);

        // Act & Assert
        var excecao = await Assert.ThrowsAsync<ApplicationException>(() => cohereService.ChatAsync(mensagem));
        Assert.Contains("Erro ao se conectar com a API Cohere", excecao.Message);
    }
}