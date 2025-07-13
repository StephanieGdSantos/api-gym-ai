using Xunit;
using Moq;
using System.Threading.Tasks;
using api_gym_ai.Adapters;
using api_gym_ai.Interfaces.Adapters;
using api_gym_ai.Interfaces.Builders;
using api_gym_ai.Models;
using api_gym_ai.Utils;
using System.Collections.Generic;
using api_gym_ai.Facades;
using System.Text.Json;
using api_gym_ai.Exceptions;
using System.Globalization;
using Microsoft.Extensions.Options;
using api_gym_ai.Options;
using api_gym_ai.Builders;

namespace api_gym_ai.Test.Builders
{
    public class PromptBuilderTests
    {
        [Fact]
        public void ComAltura_DeveAdicionarAlturaAoPrompt()
        {
            var builder = new PromptBuilder();
            builder.ComAltura("1.80");
            var prompt = builder.Build();

            Assert.Contains("Altura: 1.80", prompt.Mensagem);
        }

        [Fact]
        public void ComIdade_DeveAdicionarIdadeAoPrompt()
        {
            var builder = new PromptBuilder();
            builder.ComIdade("25");
            var prompt = builder.Build();

            Assert.Contains("Idade: 25", prompt.Mensagem);
        }

        [Fact]
        public void ComLimitacoes_NaoAdicionaQuandoNuloOuVazio()
        {
            var builder = new PromptBuilder();
            builder.ComLimitacoes(null);
            builder.ComLimitacoes("");
            var prompt = builder.Build();

            Assert.DoesNotContain("Limitações:", prompt.Mensagem);
        }

        [Fact]
        public void ComLimitacoes_AdicionaQuandoNaoVazio()
        {
            var builder = new PromptBuilder();
            builder.ComLimitacoes("Joelho");
            var prompt = builder.Build();

            Assert.Contains("Limitações: Joelho", prompt.Mensagem);
        }

        [Fact]
        public void Build_DeveSubstituirInformacoesNoPrompt()
        {
            var builder = new PromptBuilder();
            builder.ComAltura("1.75").ComIdade("30").ComPeso("80");
            var prompt = builder.Build();

            Assert.Contains("Altura: 1.75", prompt.Mensagem);
            Assert.Contains("Idade: 30", prompt.Mensagem);
            Assert.Contains("Peso: 80", prompt.Mensagem);
            Assert.DoesNotContain("[informações]", prompt.Mensagem);
        }

        [Fact]
        public void Build_DeveRemoverVirgulaFinal()
        {
            var builder = new PromptBuilder();
            builder.ComAltura("1.70").ComIdade("28");
            var prompt = builder.Build();

            // Não deve terminar com vírgula e espaço
            Assert.DoesNotMatch(@",\s*$", prompt.Mensagem);
        }
    }
}
