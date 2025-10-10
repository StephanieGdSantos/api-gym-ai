using APIGymAi.Adapters;
using APIGymAi.Adapters.Interfaces;
using APIGymAi.Builders;
using APIGymAi.Builders.Interfaces;
using APIGymAi.Facades;
using APIGymAi.Options;
using APIGymAi.Repositories;
using APIGymAi.Repositories.Interface;
using APIGymAi.RespostaSwaggerExample;
using APIGymAi.Services;
using APIGymAi.Services.Interface;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

var circuitBreakerPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30));

builder.Services.AddHttpClient<IChatRepository, CohereRepository>()
    .AddPolicyHandler(retryPolicy)
    .AddPolicyHandler(circuitBreakerPolicy);

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

builder.Services.AddScoped<ITreinoAdapter, TreinoAdapter>();
builder.Services.AddScoped<IPromptAdapter, PromptAdapter>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<ITreinoBuilder, TreinoBuilder>();
builder.Services.AddScoped<IPromptBuilder, PromptBuilder>();
builder.Services.AddScoped<IRetornoChatAdapter, RetornoChatAdapter>();

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