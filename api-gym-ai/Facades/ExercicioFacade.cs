using api_gym_ai.Builders;
using api_gym_ai.Models;
using System.Text.Json;

namespace api_gym_ai.Facades
{
    public static class ExercicioFacade
    {
        public static List<Exercicio> ListarExerciciosPropostos(string retornoChat)
        {
            var exerciciosPropostos = ExtrairRespostaDoChat(retornoChat);

            var exercicios = exerciciosPropostos.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var listaExercicios = new List<Exercicio>();
            foreach (var item in exercicios)
            {
                var partes = item.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (partes.Length == 3)
                {
                    var nomeExercicio = partes[0].Trim();
                    var numeroRepeticoes = partes[1].Split("x");
                    var series = int.Parse(numeroRepeticoes[0].Trim());
                    var repeticoes = numeroRepeticoes[1].Trim();
                    var musculosAlvo = partes[2].Trim().Split(" ");

                    var novoExercicio = new ExercicioBuilder()
                        .ComNome(nomeExercicio)
                        .ComSeries(series)
                        .ComRepeticoes(repeticoes)
                        .ComMusculosAlvo(musculosAlvo)
                        .Build();

                    listaExercicios.Add(novoExercicio);
                }
            }

            return listaExercicios;
        }

        public static string ExtrairRespostaDoChat(string retornoChat)
        {
            var conteudoJson = JsonDocument.Parse(retornoChat);
            var root = conteudoJson.RootElement;
            var conteudoGeral = root.GetProperty("message").GetProperty("content");
            var respostaChat = conteudoGeral[0].GetProperty("text").GetString();

            if (respostaChat == null)
            {
                throw new Exception("Erro ao extrair a resposta do chat.");
            }
            return respostaChat;
        }
    }
}
