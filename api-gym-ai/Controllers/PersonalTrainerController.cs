using api_gym_ai.Interfaces;
using api_gym_ai.Models;
using api_gym_ai.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace api_gym_ai.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonalTrainerController
    {
        private readonly ICohereService _cohereService;

        public PersonalTrainerController(ICohereService cohereService)
        {
            _cohereService = cohereService;
        }

        [HttpPost]
        public async Task<IEnumerable<Exercicio>> Post([FromBody] InformacoesCorporais informacoesCorporais)
        {
            var promptBase = "Haja como um personal trainer e, considerando que tenho [idade] anos, [peso]kg, [limitações], desejo trabalhar [partesDoCorpoEmFoco] para [objetivo]; sabendo disso, me diga exercícios para fazer em até [tempo] minutos na academia. me responda utilizando especificamente o formato '[exercício 1], [séries]x[repetições], [músculo(s) alvo separados por espaço]\n [exercício 2], [séries]x[repetições], [músculo(s) alvo separados por espaço]', onde os itens com chaves são mascaras para serem preenchidas; exclua as chaves do texto.não quero mais informações além das pedidas. a mensagem retornada devem seguir exatamente o modelo passado";

            var promptFinal = promptBase
                .Replace("[idade]", informacoesCorporais.Idade.ToString())
                .Replace("[peso]", informacoesCorporais.Peso.ToString())
                .Replace("[limitações]", string.Join(", ", informacoesCorporais.Limitacoes))
                .Replace("[partesDoCorpoEmFoco]", string.Join(", ", informacoesCorporais.PartesDoCorpoEmFoco))
                .Replace("[objetivo]", informacoesCorporais.Objetivo)
                .Replace("[tempo]", informacoesCorporais.TempoDeTreino.ToString());

            var jsonResponse = await _cohereService.ChatAsync(promptFinal);

            // Parse o JSON para extrair o texto
            using var document = JsonDocument.Parse(jsonResponse);
            var root = document.RootElement;
            var contentArray = root.GetProperty("message").GetProperty("content");
            var textContent = contentArray[0].GetProperty("text").GetString();

            // Agora use o texto extraído para dividir os exercícios
            var exerciciosSplit = textContent.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var listaExercicios = new List<Exercicio>();
            foreach (var item in exerciciosSplit)
            {
                var partes = item.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (partes.Length == 3)
                {
                    var nomeExercicio = partes[0].Trim();
                    var numeroRepeticoes = partes[1].Split("x");
                    var series = int.Parse(numeroRepeticoes[0].Trim());
                    var repeticoes = numeroRepeticoes[1].Trim();
                    var musculosAlvo = partes[2].Trim().Split(" ");
                        listaExercicios.Add(new Exercicio
                        {
                            Nome = nomeExercicio,
                            Series = series,
                            Repeticoes = repeticoes,
                            MusculoAlvo = musculosAlvo
                        });
                }
            }

            return listaExercicios;
        }
    }
}
