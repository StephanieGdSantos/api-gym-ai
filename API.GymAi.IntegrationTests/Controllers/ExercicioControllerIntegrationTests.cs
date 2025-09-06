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

namespace API.GymAi.IntegrationTests.Services;

public class ExercicioControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ExercicioControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
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
    public async Task Post_DeveRetornarInternalServerError_QuandoException()
    {
        // Arrange
        var pessoaValida = new Pessoa
        {
            Idade = 30,
            Altura = 1.80,
            Peso = 80,
            InfoCorporais = new InfoCorporais(),
            InfoPreferencias = new InfoPreferencias()
        };

        // Act
        var response = await _client.PostAsJsonAsync("/Exercicio", pessoaValida);

        // Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("System.NullReferenceException", content);
    }
}