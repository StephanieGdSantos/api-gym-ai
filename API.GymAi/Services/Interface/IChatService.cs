namespace API.GymAi.Services.Interface
{
    public interface IChatService
    {
        Task<string> ChatAsync(string prompt);
    }
}
