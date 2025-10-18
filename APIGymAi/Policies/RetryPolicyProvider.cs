using Polly.Extensions.Http;
using Polly;
using Polly.Contrib.WaitAndRetry;
using APIGymAi.Options;
using Microsoft.Extensions.Options;

namespace APIGymAi.Policies;

public class RetryPolicyProvider : IHttpPolicyProvider
{
    private readonly int _quantidadeMaximaDeRetentativas;
    private readonly Func<int, TimeSpan> _intervaloEmSegundosParaAguardarAntesDeTentarNovamente;

    public RetryPolicyProvider(IOptions<PolicyOptions> policyOptions)
    {
        _quantidadeMaximaDeRetentativas = policyOptions.Value.QuantidadeMaximaDeRetentativas;
        _intervaloEmSegundosParaAguardarAntesDeTentarNovamente = retryAttempt => TimeSpan.FromSeconds(
            Math.Pow(2, policyOptions.Value.IntervaloEmSegundosParaAguardarAntesDeTentarNovamente) * retryAttempt);
    }

    public IAsyncPolicy<HttpResponseMessage> GetPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(_quantidadeMaximaDeRetentativas, _intervaloEmSegundosParaAguardarAntesDeTentarNovamente);
    }
}
