namespace api_gym_ai.Interfaces.Services
{
    public interface ICohereService
    {
        Task<string> ChatAsync(string prompt);
    }
}
