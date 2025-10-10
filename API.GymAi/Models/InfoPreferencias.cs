using System.ComponentModel.DataAnnotations;

namespace APIGymAi.Models;

/// <summary>
/// Representa as preferências de treino de um usuário.
/// </summary>
public class InfoPreferencias
{
    /// <summary>
    /// Tempo mínimo permitido para o treino, em minutos.
    /// </summary>
    public const int TempoDeTreinoMin = 20;

    /// <summary>
    /// Tempo máximo permitido para o treino, em minutos.
    /// </summary>
    public const int TempoDeTreinoMax = 240;

    /// <summary>
    /// Objetivo do treino.
    /// </summary>
    [Required(ErrorMessage = "O objetivo do treino é obrigatório.")]
    public EnumObjetivo Objetivo { get; set; }

    /// <summary>
    /// Partes do corpo que o usuário deseja focar durante o treino.
    /// </summary>
    [Required(ErrorMessage = "É obrigatório informar as partes do corpo que deseja focar.")]
    public IEnumerable<EnumPartesDoCorpoEmFoco> PartesDoCorpoEmFoco { get; set; } = new List<EnumPartesDoCorpoEmFoco>();

    /// <summary>
    /// Tempo de treino em minutos.
    /// </summary>
    [Required(ErrorMessage = "O tempo de treino é obrigatório.")]
    [Range(TempoDeTreinoMin, TempoDeTreinoMax, ErrorMessage = "O tempo de treino deve ser de 20 minutos a 4 horas.")]
    public double TempoDeTreinoEmMinutos { get; set; }

    /// <summary>
    /// Tipo de variação do treino.
    /// </summary>
    [Required(ErrorMessage = "A variação de treino é obrigatória.")]
    public string VariacaoTreino { get; set; } = string.Empty;

    /// <summary>
    /// Observações adicionais sobre o treino.
    /// </summary>
    public string Observacao { get; set; } = string.Empty;

    /// <summary>
    /// Nível de condicionamento físico do usuário.
    /// </summary>
    [Required(ErrorMessage = "O nível de treino é obrigatório.")]
    public EnumNivelCondicionamento Nivel { get; set; }

    /// <summary>
    /// Enumeração das partes do corpo que podem ser focadas durante o treino.
    /// </summary>
    public enum EnumPartesDoCorpoEmFoco
    {
        /// <summary>
        /// Foco no peito.
        /// </summary>
        Peito,

        /// <summary>
        /// Foco nas costas.
        /// </summary>
        Costas,

        /// <summary>
        /// Foco nos ombros.
        /// </summary>
        Ombros,

        /// <summary>
        /// Foco nos braços.
        /// </summary>
        Bracos,

        /// <summary>
        /// Foco nas pernas.
        /// </summary>
        Pernas,

        /// <summary>
        /// Foco no abdômen.
        /// </summary>
        Abdomen,
    }

    /// <summary>
    /// Enumeração dos objetivos possíveis para o treino.
    /// </summary>
    public enum EnumObjetivo
    {
        /// <summary>
        /// Objetivo de emagrecimento.
        /// </summary>
        Emagrecimento,

        /// <summary>
        /// Objetivo de hipertrofia.
        /// </summary>
        Hipertrofia,

        /// <summary>
        /// Objetivo de definição muscular.
        /// </summary>
        DefinicaoMuscular,

        /// <summary>
        /// Objetivo de resistência.
        /// </summary>
        Resistencia,

        /// <summary>
        /// Objetivo de força.
        /// </summary>
        Forca,

        /// <summary>
        /// Objetivo de flexibilidade.
        /// </summary>
        Flexibilidade
    }

    /// <summary>
    /// Enumeração dos níveis de condicionamento físico.
    /// </summary>
    public enum EnumNivelCondicionamento
    {
        /// <summary>
        /// Nível iniciante.
        /// </summary>
        Iniciante,

        /// <summary>
        /// Nível intermediário.
        /// </summary>
        Intermediario,

        /// <summary>
        /// Nível avançado.
        /// </summary>
        Avancado
    }
}
