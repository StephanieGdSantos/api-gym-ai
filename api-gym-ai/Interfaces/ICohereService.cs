namespace api_gym_ai.Interfaces
{
    public interface ICohereService
    {
        Task<string> ChatAsync(string prompt);
    }
}
