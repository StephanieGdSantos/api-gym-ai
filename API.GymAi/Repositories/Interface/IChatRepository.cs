namespace API.GymAi.Repositories.Interface
{
    public interface IChatRepository
    {
        Task<string> SendAsync(string prompt);
    }
}
