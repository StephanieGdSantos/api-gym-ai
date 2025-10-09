using API.GymAi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.Metrics;

namespace API.GymAi.RespostaSwaggerExample
{
    public class TreinoOkExample: IExamplesProvider<Resposta<Treino>>
    {
        public Resposta<Treino> GetExamples()
        {
            return new Resposta<Treino>(
                dados: new Treino
                {
                    Periodo = new PeriodoTreino
                    {
                        DataInicio = "2024-01-01",
                        DataFim = "2024-01-31"
                    },
                    VariacaoDeTreino = new List<VariacaoDeTreino>
                    {
                        new VariacaoDeTreino
                        {
                            Dia = "Treino A",
                            MusculosTrabalhados = new List<string> { "pernas", "glúteos", "peito", "tríceps" },
                            Exercicio = new List<Exercicio>
                            {
                                new Exercicio
                                {
                                    Nome = "Supino Reto",
                                    Series = 4,
                                    Repeticoes = "5",
                                    MusculoAlvo = new List<string> { "peito", "tríceps" }
                                },
                                new Exercicio
                                {
                                    Nome = "Agachamento",
                                    Series = 4,
                                    Repeticoes = "12",
                                    MusculoAlvo = new List<string> { "pernas", "glúteos" }
                                }
                            }
                        },
                        new VariacaoDeTreino
                        {
                            Dia = "Treino B",
                            MusculosTrabalhados = new List<string> { "costas", "bíceps", "ombros" },
                            Exercicio = new List<Exercicio>
                            {
                                new Exercicio
                                {
                                    Nome = "Corrida",
                                    Series = 1,
                                    Repeticoes = "20min",
                                    MusculoAlvo = new List<string> { "cardio" }
                                },
                                new Exercicio
                                {
                                    Nome = "Burpees",
                                    Series = 3,
                                    Repeticoes = "15",
                                    MusculoAlvo = new List<string> { "corpo inteiro" }
                                }
                            }
                        }
                    }
                },
                sucesso: true,
                statusCode: 200
            );
        }
    }
}
