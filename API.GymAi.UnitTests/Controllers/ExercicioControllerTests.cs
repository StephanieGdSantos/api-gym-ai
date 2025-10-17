using APIGymAi.Adapters.Interface;
using APIGymAi.Controllers;
using APIGymAi.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace APIGymAi.UnitTests.Controllers;

public class ExercicioControllerTests
{
    private readonly Mock<ITreinoAdapter> _treinoAdapterMock;
    private readonly ExercicioController _controller;

    public ExercicioControllerTests()
    {
        _treinoAdapterMock = new Mock<ITreinoAdapter>();
        _controller = new ExercicioController(_treinoAdapterMock.Object);
    }

    [Fact]
    public async Task Post_RetornaOk_QuandoTreinoEhGerado()
    {
        // Arrange  
        var pessoa = new Pessoa();
        var treino = new Treino();
        _treinoAdapterMock.Setup(x => x.MontarTreino(pessoa)).ReturnsAsync(treino);

        // Act  
        var resultado = await _controller.Post(pessoa);

        // Assert  
        var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
        var resposta = Assert.IsType<Resposta<Treino>>(okResult.Value);
        Assert.True(resposta.Sucesso);
        Assert.Equal(HttpStatusCode.OK, resposta.StatusCode);
        Assert.Equal(treino, resposta.Dados);
    }

    [Fact]
    public async Task Post_RetornaNotFound_QuandoTreinoEhNulo()
    {
        // Arrange  
        var pessoa = new Pessoa();
        _treinoAdapterMock.Setup(x => x.MontarTreino(pessoa)).ReturnsAsync((Treino?)null);

        // Act  
        var resultado = await _controller.Post(pessoa);

        // Assert  
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(resultado.Result);
        var resposta = Assert.IsType<Resposta<Treino>>(notFoundResult.Value);
        Assert.False(resposta.Sucesso);
        Assert.Equal(HttpStatusCode.NotFound, resposta.StatusCode);
        Assert.Null(resposta.Dados);
    }

    [Fact]
    public async Task Post_RetornaInternalServerError_QuandoExcecaoEhLancada()
    {
        // Arrange  
        var pessoa = new Pessoa();
        _treinoAdapterMock.Setup(x => x.MontarTreino(pessoa)).ThrowsAsync(new Exception("Exceção de teste"));

        // Act  
        var resultado = await _controller.Post(pessoa);

        // Assert  
        //var badRequestResult = Assert.IsType<BadRequestObjectResult>(resultado.Result);
        //var resposta = Assert.IsType<Resposta<Treino>>(badRequestResult.Value);
        Assert.False(resultado.Value.Sucesso);
        Assert.Equal(HttpStatusCode.InternalServerError, resultado.Value.StatusCode);
        Assert.Null(resultado.Value.Dados);
        Assert.Contains("Exceção de teste", resultado.Value.Mensagem);
    }

    [Fact]
    public async Task Post_RetornaBadRequest_QuandoOsDadosDaPessoaSaoInvalidos()
    {
        // Arrange  
        var pessoa = new Pessoa()
        {
            Idade = 8,
            Altura = 1.75,
            Peso = 70,
            InfoCorporais = null,
            InfoPreferencias = null
        };

        _controller.ModelState.AddModelError("Idade", "A idade deve ser um valor válido, entre 10 e 100.");

        // Act  
        var resultado = await _controller.Post(pessoa);

        // Assert  
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(resultado.Result);
        var resposta = Assert.IsType<Resposta<Treino>>(badRequestResult.Value);
        Assert.False(resposta.Sucesso);
        Assert.Equal(HttpStatusCode.BadRequest, resposta.StatusCode);
        Assert.Null(resposta.Dados);
    }
}