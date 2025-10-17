using Polly;

namespace APIGymAi.Policies;

public interface IHttpPolicyProvider
{
    IAsyncPolicy<HttpResponseMessage> GetPolicy();
}
