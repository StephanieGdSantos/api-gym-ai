using APIGymAi.Adapters;
using APIGymAi.Adapters.Interface;
using APIGymAi.Builders;
using APIGymAi.Builders.Interface;
using APIGymAi.Options;
using APIGymAi.Policies;
using APIGymAi.Repositories;
using APIGymAi.Repositories.Interface;
using APIGymAi.RespostaSwaggerExample;
using APIGymAi.Services;
using APIGymAi.Services.Interface;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOptions();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerExamplesFromAssemblyOf<TreinoBadRequestExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<TreinoOkExample>();
builder.Services.AddSwaggerGen(options =>
{
     options.SwaggerDoc("v1", new OpenApiInfo
     {
         Title = "GymAi API",
         Version = "v1",
         Description = "API para geração de treinos inteligentes"
     });

    options.ExampleFilters();
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; 
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

Action<DelegateResult<HttpResponseMessage>, TimeSpan> onBreak = (result, delay) =>
{
    Console.WriteLine($"Circuito aberto em {DateTime.Now}");
};
Action onReset = () =>
{
    Console.WriteLine($"Circuito fechado em {DateTime.Now}");
};
Action onHalfOpen = () =>
{
    Console.WriteLine($"Circuito em half-open em {DateTime.Now}");
};

builder.Services.AddScoped<ITreinoAdapter, TreinoAdapter>();
builder.Services.AddScoped<IPromptAdapter, PromptAdapter>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<ITreinoBuilder, TreinoBuilder>();
builder.Services.AddScoped<IPromptBuilder, PromptBuilder>();
builder.Services.AddScoped<IRetornoChatAdapter, RetornoChatAdapter>();
builder.Services.AddScoped<RetryPolicyProvider>();

builder.Services.AddSingleton<CircuitBreakerPolicyProvider>(sp =>
    new CircuitBreakerPolicyProvider(
        sp.GetRequiredService<IOptions<PolicyOptions>>(),
        onBreak,
        onReset,
        onHalfOpen
    )
);

builder.Services.AddOptions<ChatOptions>()
    .Bind(builder.Configuration
        .GetSection("CohereConfigs"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<PeriodoDeTreinoOptions>()
    .Bind(builder.Configuration
        .GetSection("PeriodoEmDiasPorCondicionamentoFisico"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<InformacoesPromptOptions>()
    .Bind(builder.Configuration
        .GetSection("InformacoesPrompt"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<PolicyOptions>()
    .Bind(builder.Configuration
        .GetSection("PolicyOptions"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<HttpClientOptions>()
    .Bind(builder.Configuration
    .GetSection("HttpClientOptions"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddHttpClient<IChatRepository, CohereRepository>()
    .SetHandlerLifetime(TimeSpan.FromMinutes(builder.Configuration
        .GetSection("HttpClientOptions")?
        .Get<HttpClientOptions>()?.TempoDeVidaEmMinutos ?? 15))
    .AddPolicyHandler((serviceProvider, _) => serviceProvider.GetRequiredService<RetryPolicyProvider>().GetPolicy())
    .AddPolicyHandler((serviceProvider, _) => serviceProvider.GetRequiredService<CircuitBreakerPolicyProvider>().GetPolicy());


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "GymAi API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }