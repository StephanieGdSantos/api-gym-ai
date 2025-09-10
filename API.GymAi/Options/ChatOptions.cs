using System.ComponentModel.DataAnnotations;

namespace API.GymAi.Options
{
    public class ChatOptions
    {
        [Required]
        public string ApiKey { get; set; } = string.Empty;
        [Required]
        public string BaseUrl { get; set; } = string.Empty;
    }
}
