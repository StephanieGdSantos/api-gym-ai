using System.Net;
using System.Text;
using System.Text.Json;
using api_gym_ai.Models;
using api_gym_ai.Options;
using api_gym_ai.Services;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace api_gym_ai.Tests;

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
    public async Task ChatAsync_DeveRetornarNullException_SeORetornoDoChatForNulo()
    {
        //Arrange & Act
        var response = _cohereService.ChatAsync("prompt");
    
        //Assert
        await Assert.ThrowsAsync<NullReferenceException>(() => _cohereService.ChatAsync("prompt"));
    }
}