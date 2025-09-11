using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Json;
using API.GymAi;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;
using API.GymAi.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using API.GymAi.Options;
using Microsoft.Extensions.Options;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using Moq.Protected;
using API.GymAi.Services.Interface;
using API.GymAi.Services;
using API.GymAi.Repositories.Interface;
using API.GymAi.Repositories;
using API.GymAi.Adapters.Interfaces;

namespace API.GymAi.IntegrationTests.Services;

public class ExercicioControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;

    public ExercicioControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _factory = factory;
    }

    [Fact]
    public async Task Post_DeveRetornarOk_QuandoPessoaERetornoForemValidos()
    {
        // Arrange
        var pessoaValida = new Pessoa
        {
            Idade = 30,
            Altura = 1.80,
            Peso = 80,
            InfoCorporais = new InfoCorporais(),
            InfoPreferencias = new InfoPreferencias
            {
                Objetivo = InfoPreferencias.EnumObjetivo.Emagrecimento,
                PartesDoCorpoEmFoco = new List<InfoPreferencias.EnumPartesDoCorpoEmFoco> { InfoPreferencias.EnumPartesDoCorpoEmFoco.Costas },
                TempoDeTreinoEmMinutos = 60,
                VariacaoTreino = "Cardio",
                Nivel = InfoPreferencias.EnumNivelCondicionamento.Iniciante
            }
        };
        // Act
        var response = await _client.PostAsJsonAsync("/Exercicio", pessoaValida);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Post_DeveRetornarBadRequest_QuandoPessoaInvalida()
    {
        // Arrange:
        var pessoaInvalida = new Pessoa
        {
            Idade = 5,
            Altura = 1.75,
            Peso = 70,
            InfoCorporais = new InfoCorporais(),
            InfoPreferencias = new InfoPreferencias
            {
                Objetivo = InfoPreferencias.EnumObjetivo.Emagrecimento,
                PartesDoCorpoEmFoco = new List<InfoPreferencias.EnumPartesDoCorpoEmFoco> { InfoPreferencias.EnumPartesDoCorpoEmFoco.Costas },
                TempoDeTreinoEmMinutos = 60,
                VariacaoTreino = "Cardio",
                Nivel = InfoPreferencias.EnumNivelCondicionamento.Iniciante
            }
        };

        // Act
        var response = await _client.PostAsJsonAsync("/Exercicio", pessoaInvalida);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("A idade deve ser um valor válido, entre 10 e 100.", content);
    }

    [Fact]
    public async Task Post_DeveExecutarRetryQuandoServicoExternoFalha()
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            });

        var factory = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var repoDescriptors = services.Where(d => d.ServiceType == typeof(IChatRepository)).ToList();
                foreach (var descriptor in repoDescriptors)
                    services.Remove(descriptor);

                var serviceDescriptors = services.Where(d => d.ServiceType == typeof(IChatService)).ToList();
                foreach (var descriptor in serviceDescriptors)
                    services.Remove(descriptor);

                services.AddHttpClient<IChatRepository, CohereRepository>()
                    .ConfigurePrimaryHttpMessageHandler(() => handlerMock.Object);

                services.AddScoped<IChatService, ChatService>();
            });
        });

        var client = factory.CreateClient();

        // Act
        var pessoaValida = new Pessoa
        {
            Idade = 25,
            Altura = 1.75,
            Peso = 70,
            InfoCorporais = new InfoCorporais(),
            InfoPreferencias = new InfoPreferencias
            {
                Objetivo = InfoPreferencias.EnumObjetivo.Emagrecimento,
                PartesDoCorpoEmFoco = new List<InfoPreferencias.EnumPartesDoCorpoEmFoco> { InfoPreferencias.EnumPartesDoCorpoEmFoco.Costas },
                TempoDeTreinoEmMinutos = 60,
                VariacaoTreino = "Cardio",
                Nivel = InfoPreferencias.EnumNivelCondicionamento.Iniciante
            }
        };
        var response = await client.PostAsJsonAsync("/Exercicio", pessoaValida);

        // Assert
        handlerMock.Protected().Verify(
            "SendAsync",
            Times.AtLeast(2),
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task Post_DeveAbrirCircuitBreaker_AposFalhasConsecutivas()
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            });

        var factory = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var repoDescriptors = services.Where(d => d.ServiceType == typeof(IChatRepository)).ToList();
                foreach (var descriptor in repoDescriptors)
                    services.Remove(descriptor);

                services.AddHttpClient<IChatRepository, CohereRepository>()
                    .ConfigurePrimaryHttpMessageHandler(() => handlerMock.Object);
                services.AddScoped<IChatService, ChatService>();
            });
        });

        var client = factory.CreateClient();
        var pessoaValida = new Pessoa
        {
            Idade = 25,
            Altura = 1.75,
            Peso = 70,
            InfoCorporais = new InfoCorporais(),
            InfoPreferencias = new InfoPreferencias
            {
                Objetivo = InfoPreferencias.EnumObjetivo.Emagrecimento,
                PartesDoCorpoEmFoco = new List<InfoPreferencias.EnumPartesDoCorpoEmFoco> { InfoPreferencias.EnumPartesDoCorpoEmFoco.Costas },
                TempoDeTreinoEmMinutos = 60,
                VariacaoTreino = "Cardio",
                Nivel = InfoPreferencias.EnumNivelCondicionamento.Iniciante
            }
        };

        for (int i = 0; i < 5; i++)
        {
            var response = await client.PostAsJsonAsync("/Exercicio", pessoaValida);
        }

        handlerMock.Protected().Verify(
            "SendAsync",
            Times.Exactly(3),
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>());
    }
}