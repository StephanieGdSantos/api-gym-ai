using System.ComponentModel.DataAnnotations;

namespace API.GymAi.Options
{
    /// <summary>
    /// Represents the configuration options for the chat functionality.
    /// </summary>
    public class ChatOptions
    {
        /// <summary>
        /// Gets or sets the API key required for authentication.
        /// </summary>
        [Required]
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the base URL for the chat API.
        /// </summary>
        [Required]
        public string BaseUrl { get; set; } = string.Empty;
    }
}
