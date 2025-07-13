using System.Net;
using System.Text;
using System.Text.Json;
using api_gym_ai.Models;
using api_gym_ai.Options;
using api_gym_ai.Services;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace api_gym_ai.Test.Services;

public class CohereServiceTests
{
    private readonly Mock<HttpClient> _mockHttpClient;
    private readonly Mock<IOptions<CohereServiceOptions>> _mockCohereServiceOptions;
    private readonly CohereService _cohereService;

    public CohereServiceTests()
    {
        _mockHttpClient = new Mock<HttpClient>();
        _mockCohereServiceOptions = new Mock<IOptions<CohereServiceOptions>>();
        _mockCohereServiceOptions.Setup(x => x.Value).Returns(new CohereServiceOptions
        {
            ApiKey = "fake-api-key",
            BaseUrl = "https://fake-url"
        });


        _cohereService = new CohereService(_mockHttpClient.Object, _mockCohereServiceOptions.Object);
    }

    [Fact]
    public async Task ChatAsync_DeveRetornarException_SeORetornoDoChatForNulo()
    {
        //Arrange & Act
        var response = _cohereService.ChatAsync(It.IsAny<string>());
    
        //Assert
        await Assert.ThrowsAsync<Exception>(() => _cohereService.ChatAsync("prompt"));
    }

    [Fact]
    public async Task ChatAsync_DeveLancarHttpRequestException_QuandoStatusNaoForSucesso()
    {
        // Arrange
        var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent("{\"error\":\"bad request\"}", Encoding.UTF8, "application/json")
            });

        var httpClient = new HttpClient(httpMessageHandlerMock.Object);
        var cohereService = new CohereService(httpClient, _mockCohereServiceOptions.Object);

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => cohereService.ChatAsync("prompt"));
    }

    [Fact]
    public async Task ChatAsync_DeveRetornarRespostaDoChat_QuandoAChamadaForBemSucedida()
    {
        // Arrange
        var mensagemEsperada = "{\"message\":\"ok\"}";

        var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(mensagemEsperada, Encoding.UTF8, "application/json")
            });

        var httpClient = new HttpClient(httpMessageHandlerMock.Object);

        var cohereService = new CohereService(httpClient, _mockCohereServiceOptions.Object);

        // Act
        var resultado = await cohereService.ChatAsync("prompt");

        // Assert
        Assert.Equal(mensagemEsperada, resultado);
    }

}