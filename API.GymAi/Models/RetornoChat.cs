namespace API.GymAi.Models
{
    public class RetornoChat
    {
        public string id { get; set; }
        public Message message { get; set; }
    }

    public class Message
    {
        public string role { get; set; }
        public Content[] content { get; set; }
    }

    public class Content
    {
        public string type { get; set; }
        public string text { get; set; }
    }
}
