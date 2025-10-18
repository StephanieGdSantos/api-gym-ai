using APIGymAi.Models;

namespace APIGymAi.Builders.Interface;

/// <summary>  
/// Interface para construir prompts com informações fornecidas.  
/// </summary>  
public interface IPromptBuilder
{
    /// <summary>  
    /// Define o objetivo do treino.  
    /// </summary>  
    /// <param name="objetivo">O objetivo do treino.</param>  
    /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento.</returns>  
    public IPromptBuilder ComObjetivo(string objetivo);

    /// <summary>  
    /// Define o tempo de treino em minutos.  
    /// </summary>  
    /// <param name="tempoDeTreino">O tempo de treino em minutos.</param>  
    /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento.</returns>  
    public IPromptBuilder ComTempoDeTreinoEmMinutos(string tempoDeTreino);

    /// <summary>  
    /// Define as partes do corpo em foco.  
    /// </summary>  
    /// <param name="partesDoCorpoEmFoco">As partes do corpo em foco.</param>  
    /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento.</returns>  
    public IPromptBuilder ComPartesDoCorpoEmFoco(string partesDoCorpoEmFoco);

    /// <summary>  
    /// Define as limitações do usuário.  
    /// </summary>  
    /// <param name="limitacoes">As limitações do usuário.</param>  
    /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento.</returns>  
    public IPromptBuilder ComLimitacoes(string? limitacoes);

    /// <summary>  
    /// Define o sexo do usuário.  
    /// </summary>  
    /// <param name="sexo">O sexo do usuário.</param>  
    /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento.</returns>  
    public IPromptBuilder ComSexo(string? sexo);

    /// <summary>  
    /// Define a idade do usuário.  
    /// </summary>  
    /// <param name="idade">A idade do usuário.</param>  
    /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento.</returns>  
    public IPromptBuilder ComIdade(string idade);

    /// <summary>  
    /// Define a altura do usuário.  
    /// </summary>  
    /// <param name="altura">A altura do usuário.</param>  
    /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento.</returns>  
    public IPromptBuilder ComAltura(string altura);

    /// <summary>  
    /// Define o peso do usuário.  
    /// </summary>  
    /// <param name="peso">O peso do usuário.</param>  
    /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento.</returns>  
    public IPromptBuilder ComPeso(string peso);

    /// <summary>  
    /// Define a massa muscular do usuário.  
    /// </summary>  
    /// <param name="massaMuscular">A massa muscular do usuário.</param>  
    /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento.</returns>  
    public IPromptBuilder ComMassaMuscular(string? massaMuscular);

    /// <summary>  
    /// Define o percentual de gordura do usuário.  
    /// </summary>  
    /// <param name="percentualGordura">O percentual de gordura do usuário.</param>  
    /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento.</returns>  
    public IPromptBuilder ComPercentualDeGordura(string? percentualGordura);

    /// <summary>  
    /// Define a variação de treino.  
    /// </summary>  
    /// <param name="variacao">A variação de treino.</param>  
    /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento.</returns>  
    public IPromptBuilder ComVariacaoDeTreino(string variacao);

    /// <summary>  
    /// Define a observação sobre o treino.  
    /// </summary>  
    /// <param name="variacaoMuscular">A observação sobre o treino.</param>  
    /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento.</returns>  
    public IPromptBuilder ComObservacao(string variacaoMuscular);

    /// <summary>  
    /// Define o nível de condicionamento do usuário.  
    /// </summary>  
    /// <param name="nivel">O nível de condicionamento.</param>  
    /// <returns>O próprio <see cref="IPromptBuilder"/> para encadeamento.</returns>  
    public IPromptBuilder ComNivel(string nivel);

    /// <summary>  
    /// Constrói o prompt com as informações fornecidas.  
    /// </summary>  
    /// <returns>O prompt construído.</returns>  
    public Prompt Build();
}
