namespace API.GymAi.Models
{
    /// <summary>  
    /// Representa o retorno de um chat, contendo um identificador e uma mensagem.  
    /// </summary>  
    public class RetornoChat
    {
        /// <summary>  
        /// Identificador único do retorno do chat.  
        /// </summary>  
        public string id { get; set; }

        /// <summary>  
        /// Mensagem associada ao retorno do chat.  
        /// </summary>  
        public Message message { get; set; }
    }

    /// <summary>  
    /// Representa uma mensagem no contexto do chat.  
    /// </summary>  
    public class Message
    {
        /// <summary>  
        /// Papel associado à mensagem (ex.: usuário, assistente).  
        /// </summary>  
        public string role { get; set; }

        /// <summary>  
        /// Conteúdo da mensagem, podendo conter múltiplos itens.  
        /// </summary>  
        public Content[] content { get; set; }
    }

    /// <summary>  
    /// Representa o conteúdo de uma mensagem.  
    /// </summary>  
    public class Content
    {
        /// <summary>  
        /// Tipo do conteúdo (ex.: texto, imagem).  
        /// </summary>  
        public string type { get; set; }

        /// <summary>  
        /// Texto associado ao conteúdo.  
        /// </summary>  
        public string text { get; set; }
    }
}
