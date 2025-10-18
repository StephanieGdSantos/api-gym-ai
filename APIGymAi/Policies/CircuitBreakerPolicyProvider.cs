using Polly.Extensions.Http;
using Polly;
using APIGymAi.Options;
using Microsoft.Extensions.Options;

namespace APIGymAi.Policies;

public delegate void CircuitBreakAction(DelegateResult<HttpResponseMessage> result, TimeSpan breakDelay);
public delegate void CircuitAction();

public class CircuitBreakerPolicyProvider : IHttpPolicyProvider
{
    private readonly int _numeroMaximoDeExcecoesAntesDeCair;
    private readonly TimeSpan _intervaloDeQuedaEmSegundos;
    private readonly Action<DelegateResult<HttpResponseMessage>, TimeSpan> _onBreak;
    private readonly Action _onReset;
    private readonly Action _onHalfOpen;

    public CircuitBreakerPolicyProvider(
        IOptions<PolicyOptions> policyOptions,
        Action<DelegateResult<HttpResponseMessage>, TimeSpan>? onBreak = null,
        Action? onReset = null,
        Action? onHalfOpen = null)
    {
        _numeroMaximoDeExcecoesAntesDeCair = policyOptions.Value.NumeroMaximoDeExcecoesAntesDeCair;
        _intervaloDeQuedaEmSegundos = TimeSpan.FromSeconds(policyOptions.Value.IntervaloDeQuedaEmSegundos);
        _onBreak = onBreak ?? ((result, delay) => { });
        _onReset = onReset ?? (() => { });
        _onHalfOpen = onHalfOpen ?? (() => { });
    }

    public IAsyncPolicy<HttpResponseMessage> GetPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => !msg.IsSuccessStatusCode)
            .CircuitBreakerAsync(
                _numeroMaximoDeExcecoesAntesDeCair,
                _intervaloDeQuedaEmSegundos,
                onBreak: (result, delay) => _onBreak(result, delay),
                onReset: () => _onReset(),
                onHalfOpen: () => _onHalfOpen()
            );
    }
}
